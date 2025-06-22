using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.ServiceQueries
{
    public record class ListServicesQuery() : IRequest<ErrorOr<List<ServiceDto>>>;

    public class ListServicesQueryHandler : IRequestHandler<ListServicesQuery, ErrorOr<List<ServiceDto>>>
    {
        private readonly IRepository<Service, Guid> _repository;
        private readonly IMapper _mapper;

        public ListServicesQueryHandler(IRepository<Service, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<ServiceDto>>> Handle(ListServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _repository.ListAsync(cancellationToken);
            return _mapper.Map<List<ServiceDto>>(services);
        }
    }
} 