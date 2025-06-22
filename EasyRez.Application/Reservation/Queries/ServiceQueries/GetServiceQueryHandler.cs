using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Application.Reservation.Common.Specifications;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.ServiceQueries
{
    public record class GetServiceQuery(Guid Id) : IRequest<ErrorOr<ServiceDto>>;

    public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, ErrorOr<ServiceDto>>
    {
        private readonly IRepository<Service, Guid> _repository;
        private readonly IMapper _mapper;

        public GetServiceQueryHandler(IRepository<Service, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ServiceDto>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            var spec = new ServiceSpecification(request.Id);
            var service = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (service is null)
                return Error.NotFound();
            return _mapper.Map<ServiceDto>(service);
        }
    }
} 