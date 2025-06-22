namespace EasyRez.Application.Reservation.Common.Models
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public IReadOnlyCollection<ServiceDto> Services { get; set; } = new List<ServiceDto>();
    }
} 