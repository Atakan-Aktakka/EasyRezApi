using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.AppointmentQueries
{
    public record class ListAppointmentsQuery() : IRequest<ErrorOr<List<AppointmentDto>>>;

    public class ListAppointmentsQueryHandler : IRequestHandler<ListAppointmentsQuery, ErrorOr<List<AppointmentDto>>>
    {
        private readonly IRepository<Appointment, Guid> _repository;
        private readonly IMapper _mapper;

        public ListAppointmentsQueryHandler(IRepository<Appointment, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<AppointmentDto>>> Handle(ListAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _repository.ListAsync(cancellationToken);
            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
}