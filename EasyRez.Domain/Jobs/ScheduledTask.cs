using EasyRez.Core; // Sizin EntityBase'iniz için

namespace EasyRez.Domain.Jobs
{
    // Hangi aralıklarla çalışacağını belirtmek için bir enum
    public enum JobIntervalType
    {
        Minutes,
        Hours,
        Days
    }

    public class ScheduledTask : Entity<Guid> // Veya sizin temel varlık sınıfınız
    {
        public string UserId { get; private set; } // Hangi kullanıcıya ait olduğu
        public string HttpMethod { get; private set; } // "GET", "POST", vb.
        public string Url { get; private set; } // Çağırılacak URL
        public string? Payload { get; private set; } // POST/PUT için JSON verisi
        public JobIntervalType IntervalType { get; private set; }
        public int IntervalValue { get; private set; } // Örn: Days ve 5 = 5 günde bir
        public DateTime NextRunTime { get; set; } // Bir sonraki çalışma zamanı (En önemlisi)
        public bool IsActive { get; set; } = true;
        public DateTime? LastRunTime { get; set; }
        public string? LastRunStatus { get; set; }

        // EF Core için boş constructor
        private ScheduledTask() { } 

        // Yeni görev oluşturmak için fabrika metodu
        public static ScheduledTask Create(string userId, string httpMethod, string url, string? payload, JobIntervalType intervalType, int intervalValue)
        {
            return new ScheduledTask
            {
                UserId = userId,
                HttpMethod = httpMethod.ToUpper(),
                Url = url,
                Payload = payload,
                IntervalType = intervalType,
                IntervalValue = intervalValue,
                IsActive = true,
                // İlk çalışma zamanını hemen veya belirli bir süre sonra başlatabilirsiniz
                // Şimdilik 1 dakika sonra başlasın diyelim:
                NextRunTime = DateTime.UtcNow.AddMinutes(1) 
            };
        }

        // Görevi çalıştıktan sonra bir sonraki çalışma zamanını hesaplar
        public void CalculateNextRunTime()
        {
            var now = DateTime.UtcNow;
            switch (IntervalType)
            {
                case JobIntervalType.Minutes:
                    NextRunTime = now.AddMinutes(IntervalValue);
                    break;
                case JobIntervalType.Hours:
                    NextRunTime = now.AddHours(IntervalValue);
                    break;
                case JobIntervalType.Days:
                    NextRunTime = now.AddDays(IntervalValue);
                    break;
                default:
                    // Bilinmeyen bir tipse, bir daha çalışmasın
                    IsActive = false; 
                    break;
            }
        }
    }
}