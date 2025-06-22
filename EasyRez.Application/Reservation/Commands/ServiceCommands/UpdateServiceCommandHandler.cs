using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.ServiceCommands
{
    public record class UpdateServiceCommand(
        Guid Id,
        string Name,
        Guid CustomerId,
        string Description,
        TimeSpan Duration,
        TimeSpan? ReminderAfter
    ) : IRequest<ErrorOr<Guid>>;

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Service, Guid> _repository;
        public UpdateServiceCommandHandler(IRepository<Service, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<Guid>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (service is null)
                return Error.NotFound();
            service.Update(request.Name, request.CustomerId, request.Description, request.Duration, request.ReminderAfter);
            await _repository.UpdateAsync(service, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return service.Id;
        }
    }
}