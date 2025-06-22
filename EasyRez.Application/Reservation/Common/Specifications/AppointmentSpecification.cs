using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using EasyRez.Domain.Reservation;

namespace EasyRez.Application.Reservation.Common.Specifications
{
    public class AppointmentSpecification : Specification<Appointment>, ISingleResultSpecification<Appointment>
    {
        public AppointmentSpecification() { }

        public AppointmentSpecification(Guid id) : this()
        {
            Query.Where(x => x.Id == id);
        }

        public AppointmentSpecification ByCustomerId(Guid customerId)
        {
            Query.Where(x => x.CustomerId == customerId);
            return this;
        }

        public AppointmentSpecification IncludeCustomer()
        {
            Query.Include(x => x.CustomerId);
            return this;
        }

        public AppointmentSpecification NoTracking()
        {
            Query.AsNoTracking();
            return this;
        }
    }
}