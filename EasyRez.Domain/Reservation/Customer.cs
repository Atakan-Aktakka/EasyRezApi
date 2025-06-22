using EasyRez.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Domain.Reservation
{
    public class Customer : Entity<Guid>
    {
        public string? FullName { get; private set; }
        public string? PhoneNumber { get; private set; }
        private readonly List<Appointment> _appointments = [];
        public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
        
        private Customer() {}
        
        private Customer(Guid id, string fullName, string phoneNumber):base(id)
        {
            Id = id;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
        
        public static Customer Create(string fullName, string phoneNumber)
        {
            return new Customer(Guid.NewGuid(), fullName, phoneNumber);
        }
        
        public void Update(string fullName, string phoneNumber)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
        
        public void AddAppointment(Appointment appointment)
        {
            _appointments.Add(appointment);
        }
    }
}
