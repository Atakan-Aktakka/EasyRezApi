using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.CustomerQueries
{
    public record class ListCustomersQuery() : IRequest<ErrorOr<List<CustomerDto>>>;

    public class ListCustomersQueryHandler : IRequestHandler<ListCustomersQuery, ErrorOr<List<CustomerDto>>>
    {
        private readonly IRepository<Customer, Guid> _repository;
        private readonly IMapper _mapper;

        public ListCustomersQueryHandler(IRepository<Customer, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<CustomerDto>>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.ListAsync(cancellationToken);
            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
} 