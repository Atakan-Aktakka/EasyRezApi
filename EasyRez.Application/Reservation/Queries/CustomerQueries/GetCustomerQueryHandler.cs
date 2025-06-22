using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Application.Reservation.Common.Specifications;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.CustomerQueries
{
    public record class GetCustomerQuery(Guid Id) : IRequest<ErrorOr<CustomerDto>>;

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ErrorOr<CustomerDto>>
    {
        private readonly IRepository<Customer, Guid> _repository;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IRepository<Customer, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerSpecification(request.Id);
            var customer = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (customer is null)
                return Error.NotFound();
            return _mapper.Map<CustomerDto>(customer);
        }
    }
} 