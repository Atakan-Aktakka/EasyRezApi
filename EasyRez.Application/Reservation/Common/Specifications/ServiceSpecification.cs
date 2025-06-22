using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using EasyRez.Domain.Reservation;

namespace EasyRez.Application.Reservation.Common.Specifications
{
    public class ServiceSpecification : Specification<Service>, ISingleResultSpecification<Service>
    {
        public ServiceSpecification() { }

        public ServiceSpecification(Guid id) : this()
        {
            Query.Where(x => x.Id == id);
        }

        public ServiceSpecification ByCustomerId(Guid customerId)
        {
            Query.Where(x => x.CustomerId == customerId);
            return this;
        }

        public ServiceSpecification IncludeCustomer()
        {
            Query.Include(x => x.CustomerId);
            return this;
        }

        public ServiceSpecification NoTracking()
        {
            Query.AsNoTracking();
            return this;
        }
    }
}