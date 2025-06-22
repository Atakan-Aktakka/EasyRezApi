using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.CustomerCommands
{
    public record class CreateCustomerCommand(
        string FullName,
        string PhoneNumber
    ) : IRequest<ErrorOr<Guid>>;

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Customer, Guid> _repository;

        public CreateCustomerCommandHandler(IRepository<Customer, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create(request.FullName, request.PhoneNumber);
            await _repository.AddAsync(customer, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}