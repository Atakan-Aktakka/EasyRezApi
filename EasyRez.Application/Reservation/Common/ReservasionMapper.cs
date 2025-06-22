using AutoMapper;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Domain.Reservation;

namespace EasyRez.Application.Reservation.Common
{
    public class ReservasionMapper:Profile
    {
        public ReservasionMapper()
        {
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Service, ServiceDto>();
        }
         
    }
}