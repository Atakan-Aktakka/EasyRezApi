using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.AppointmentCommands
{
    public record class CreateAppointmentCommand(
        Guid CustomerId,
        DateTime Date,
        string? Notes
    ) : IRequest<ErrorOr<Guid>>;

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Appointment, Guid> _repository;
        public CreateAppointmentCommandHandler(IRepository<Appointment, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = Appointment.Create(request.CustomerId, request.Date);
            if (!string.IsNullOrWhiteSpace(request.Notes))
                appointment.AddNotes(request.Notes);
            await _repository.AddAsync(appointment, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return appointment.Id;
        }
    }
}