using EasyRez.Infrastructure.Persistence; // DbContext'iniz için
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options; // Ayarlar için
using System.Net.Http;

namespace EasyRez.Api.Workers
{
    public class ExternalApiWorker : BackgroundService
    {
        private readonly ILogger<ExternalApiWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WorkerSettings _settings;

        public ExternalApiWorker(
            ILogger<ExternalApiWorker> logger,
            IServiceScopeFactory scopeFactory,
            IHttpClientFactory httpClientFactory,
            IOptions<WorkerSettings> settings)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("External API Worker başlatıldı.");
            
            if (_settings.IntervalInMinutes <= 0)
            {
                _logger.LogError("Worker interval süresi geçersiz. Worker durduruluyor.");
                return;
            }
            
            _logger.LogInformation($"Worker {_settings.IntervalInMinutes} dakikada bir çalışacak.");
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(_settings.IntervalInMinutes));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    try
                    {
                        _logger.LogInformation("Worker çalışıyor: EasyRez API'ye GET isteği atılıyor...");
                        
                        var httpClient = _httpClientFactory.CreateClient("EasyRezClient");
                        
                        // Örnek: Kendi API'nızdaki bir endpoint'e (örn: randevular) istek at
                        var response = await httpClient.GetAsync("/api/Reservation/Appointment", stoppingToken);

                        if (response.IsSuccessStatusCode)
                        {
                            _logger.LogInformation("API'den başarılı yanıt alındı.");
                            
                            // Gerekirse yanıtı veritabanına loglayabilirsiniz
                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var db = scope.ServiceProvider.GetRequiredService<EasyRezDbContext>();
                                // ... db.Logs.Add(...) ...
                                // await db.SaveChangesAsync(stoppingToken);
                            }
                        }
                        else
                        {
                            _logger.LogWarning($"API isteği başarısız oldu: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex) when (ex is not OperationCanceledException)
                    {
                        _logger.LogError(ex, "Worker döngüsünde bir hata oluştu.");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("External API Worker durduruluyor.");
            }
        }
    }
}