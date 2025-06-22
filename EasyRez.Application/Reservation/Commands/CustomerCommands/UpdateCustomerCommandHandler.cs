using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.CustomerCommands
{
    public record class UpdateCustomerCommand(
        Guid Id,
        string FullName,
        string PhoneNumber
    ) : IRequest<ErrorOr<Guid>>;

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Guid>>
    {
        private readonly IRepository<Customer, Guid> _repository;
        public UpdateCustomerCommandHandler(IRepository<Customer, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ErrorOr<Guid>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null)
                return Error.NotFound();
            customer.Update(request.FullName, request.PhoneNumber);
            await _repository.UpdateAsync(customer, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}