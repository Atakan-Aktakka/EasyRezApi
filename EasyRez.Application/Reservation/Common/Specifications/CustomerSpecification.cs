using Ardalis.Specification;
using EasyRez.Domain.Reservation;


namespace EasyRez.Application.Reservation.Common.Specifications
{
    public class CustomerSpecification : Specification<Customer>, ISingleResultSpecification<Customer>
    {
        public CustomerSpecification() { }

        public CustomerSpecification(Guid id)
        {
            Query.Where(entity => entity.Id == id);
        }

        public CustomerSpecification ByPhoneNumber(string phoneNumber)
        {
            Query.Where(entity => entity.PhoneNumber == phoneNumber);
            return this;
        }

        public CustomerSpecification ByFullName(string fullName)
        {
            Query.Where(entity => entity.FullName == fullName);
            return this;
        }

        public CustomerSpecification IncludeAppointments()
        {
            Query.Include(entity => entity.Appointments);
            return this;
        }

        public CustomerSpecification NoTracking()
        {
            Query.AsNoTracking();
            return this;
        }
    }
} 