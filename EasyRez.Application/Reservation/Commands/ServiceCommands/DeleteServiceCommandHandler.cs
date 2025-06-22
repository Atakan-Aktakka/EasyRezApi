using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Commands.ServiceCommands
{
public record class DeleteServiceCommand(
    Guid Id
) : IRequest<ErrorOr<Unit>>;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Service, Guid> _repository;
    public DeleteServiceCommandHandler(IRepository<Service, Guid> repository)
    {
        _repository = repository;
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (service is null)
            return Error.NotFound();
        await _repository.DeleteAsync(service, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
} 
}