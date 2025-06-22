using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.AppointmentCommands
{

    public record class DeleteAppointmentCommand(
        Guid Id
    ) : IRequest<ErrorOr<Unit>>;

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, ErrorOr<Unit>>
    {
        private readonly IRepository<Appointment, Guid> _repository;
        public DeleteAppointmentCommandHandler(IRepository<Appointment, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<Unit>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (appointment is null)
                return Error.NotFound();
            await _repository.DeleteAsync(appointment, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}