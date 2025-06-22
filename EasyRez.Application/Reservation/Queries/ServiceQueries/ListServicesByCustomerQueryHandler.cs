using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Application.Reservation.Common.Specifications;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.ServiceQueries
{
    public record class ListServicesByCustomerQuery(Guid CustomerId) : IRequest<ErrorOr<List<ServiceDto>>>;

    public class ListServicesByCustomerQueryHandler : IRequestHandler<ListServicesByCustomerQuery, ErrorOr<List<ServiceDto>>>
    {
        private readonly IRepository<Service, Guid> _repository;
        private readonly IMapper _mapper;

        public ListServicesByCustomerQueryHandler(IRepository<Service, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<ServiceDto>>> Handle(ListServicesByCustomerQuery request, CancellationToken cancellationToken)
        {
            var spec = new ServiceSpecification().ByCustomerId(request.CustomerId);
            var services = await _repository.ListAsync(spec, cancellationToken);
            return _mapper.Map<List<ServiceDto>>(services);
        }
    }
} 