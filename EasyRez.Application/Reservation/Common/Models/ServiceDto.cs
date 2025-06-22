namespace EasyRez.Application.Reservation.Common.Models
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public string Description { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public TimeSpan? ReminderAfter { get; set; }
    }
} 