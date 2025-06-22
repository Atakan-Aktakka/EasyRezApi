using AutoMapper;
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Application.Reservation.Common.Specifications;
using EasyRez.Domain.Reservation;
using ErrorOr;
using MediatR;

namespace EasyRez.Application.Reservation.Queries.AppointmentQueries
{
    public record class GetAppointmentQuery(Guid Id) : IRequest<ErrorOr<AppointmentDto>>;

    public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, ErrorOr<AppointmentDto>>
    {
        private readonly IRepository<Appointment, Guid> _repository;
        private readonly IMapper _mapper;

        public GetAppointmentQueryHandler(IRepository<Appointment, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<AppointmentDto>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            var spec = new AppointmentSpecification(request.Id);
            var appointment = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (appointment is null)
                return Error.NotFound();
            return _mapper.Map<AppointmentDto>(appointment);
        }
    }
}