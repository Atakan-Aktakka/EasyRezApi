using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.AppointmentCommands
{
    public record class UpdateAppointmentCommand(
        Guid Id,
        Guid CustomerId,
        DateTime Date,
        string? Notes
    ) : IRequest<ErrorOr<Guid>>;

    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Appointment, Guid> _repository;
        public UpdateAppointmentCommandHandler(IRepository<Appointment, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<Guid>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (appointment is null)
                return Error.NotFound();
            appointment.Update(request.CustomerId, request.Date, request.Notes ?? "");
            await _repository.UpdateAsync(appointment, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return appointment.Id;
        }
    }
}