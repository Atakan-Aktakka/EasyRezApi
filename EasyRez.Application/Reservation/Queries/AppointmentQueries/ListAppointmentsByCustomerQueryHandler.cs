using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Application.Reservation.Common.Specifications;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.AppointmentQueries
{
    public record class ListAppointmentsByCustomerQuery(Guid CustomerId) : IRequest<ErrorOr<List<AppointmentDto>>>;

    public class ListAppointmentsByCustomerQueryHandler : IRequestHandler<ListAppointmentsByCustomerQuery, ErrorOr<List<AppointmentDto>>>
    {
        private readonly IRepository<Appointment, Guid> _repository;
        private readonly IMapper _mapper;

        public ListAppointmentsByCustomerQueryHandler(IRepository<Appointment, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<AppointmentDto>>> Handle(ListAppointmentsByCustomerQuery request, CancellationToken cancellationToken)
        {
            var spec = new AppointmentSpecification().ByCustomerId(request.CustomerId);
            var appointments = await _repository.ListAsync(spec, cancellationToken);
            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
} 