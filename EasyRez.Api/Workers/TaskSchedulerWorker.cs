using EasyRez.Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EasyRez.Domain.Jobs;

namespace EasyRez.Api.Workers
{
    public class TaskSchedulerWorker : BackgroundService
    {
        private readonly ILogger<TaskSchedulerWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TimeSpan _tickInterval;

        public TaskSchedulerWorker(
            ILogger<TaskSchedulerWorker> logger,
            IServiceScopeFactory scopeFactory,
            IHttpClientFactory httpClientFactory,
            IOptions<WorkerSettings> settings)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _httpClientFactory = httpClientFactory;
            _tickInterval = TimeSpan.FromMinutes(settings.Value.SchedulerTickIntervalInMinutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Task Scheduler Worker başlatıldı. Kontrol aralığı: {_tickInterval.TotalMinutes} dk.");
            
            using var timer = new PeriodicTimer(_tickInterval);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    _logger.LogInformation("Zamanı gelmiş görevler aranıyor...");
                    await RunDueTasksAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Task Scheduler Worker durduruluyor.");
            }
        }

        private async Task RunDueTasksAsync(CancellationToken stoppingToken)
        {
            // Her döngüde YENİ bir scope açmak ZORUNLUDUR.
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EasyRezDbContext>();
                
                var now = DateTime.UtcNow;

                // Vakti gelmiş ve aktif olan görevleri bul
                var tasksToRun = await db.Set<ScheduledTask>() // <-- DOĞRUSU
                    .Where(t => t.IsActive && t.NextRunTime <= now)
                    .ToListAsync(stoppingToken);

                if (!tasksToRun.Any())
                {
                    _logger.LogInformation("Çalıştırılacak görev bulunamadı.");
                    return;
                }
                
                _logger.LogInformation($"{tasksToRun.Count} adet görev çalıştırılacak.");
                
                // Artık "EasyRezClient" değil, GENEL bir client kullanıyoruz
                var httpClient = _httpClientFactory.CreateClient();

                foreach (var task in tasksToRun)
                {
                    try
                    {
                        // Görevi çalıştır
                        var response = await ExecuteTaskAsync(httpClient, task, stoppingToken);

                        // Başarılı/Başarısız durumunu kaydet
                        task.LastRunStatus = $"{(int)response.StatusCode}: {response.ReasonPhrase}";
                        _logger.LogInformation($"Görev {task.Id} çalıştı: {task.LastRunStatus}");
                    }
                    catch (Exception ex)
                    {
                        task.LastRunStatus = $"Hata: {ex.Message}";
                        _logger.LogError(ex, $"Görev {task.Id} çalıştırılırken HATA oluştu.");
                    }
                    
                    // Görevi güncelle: Son çalışma zamanı ve bir SONRAKİ çalışma zamanı
                    task.LastRunTime = DateTime.UtcNow;
                    task.CalculateNextRunTime(); // Bu metot 'NextRunTime'ı günceller
                }

                // Tüm güncellemeleri veritabanına kaydet
                await db.SaveChangesAsync(stoppingToken);
            }
        }

        private async Task<HttpResponseMessage> ExecuteTaskAsync(HttpClient client, Domain.Jobs.ScheduledTask task, CancellationToken stoppingToken)
        {
            // URL'nin tam (absolute) bir URL olduğundan emin olun
            if (!Uri.IsWellFormedUriString(task.Url, UriKind.Absolute))
            {
                throw new InvalidOperationException("Geçersiz URL formatı. Mutlak URL bekleniyordu.");
            }

            var request = new HttpRequestMessage(new HttpMethod(task.HttpMethod), task.Url);

            // Eğer POST veya PUT ise, Payload'ı (JSON) ekle
            if ((task.HttpMethod == "POST" || task.HttpMethod == "PUT") && !string.IsNullOrEmpty(task.Payload))
            {
                request.Content = new StringContent(task.Payload, Encoding.UTF8, "application/json");
            }

            // (İsteğe bağlı) Bu isteğe, görevi başlatan kullanıcı adına bir
            // Authentication (JWT) header'ı da ekleyebilirsiniz.
            // request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "kullanıcının_token'ı");

            return await client.SendAsync(request, stoppingToken);
        }
    }
}