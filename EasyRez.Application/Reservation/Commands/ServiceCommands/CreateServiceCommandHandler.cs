using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.ServiceCommands
{
    public record class CreateServiceCommand(
        string Name,
        Guid CustomerId,
        string Description,
        TimeSpan Duration,
        TimeSpan? ReminderAfter
    ) : IRequest<ErrorOr<Guid>>;

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Service, Guid> _repository;
        public CreateServiceCommandHandler(IRepository<Service, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<Guid>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = Service.Create(request.Name, request.CustomerId, request.Description, request.Duration, request.ReminderAfter);
            await _repository.AddAsync(service, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return service.Id;
        }
    }
}