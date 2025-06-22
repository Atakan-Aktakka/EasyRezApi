using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.CustomerCommands
{

public record class DeleteCustomerCommand(
    Guid Id
) : IRequest<ErrorOr<Unit>>;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Customer, Guid> _repository;
    public DeleteCustomerCommandHandler(IRepository<Customer, Guid> repository)
    {
        _repository = repository;
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
            return Error.NotFound();
        await _repository.DeleteAsync(customer, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
} 
}