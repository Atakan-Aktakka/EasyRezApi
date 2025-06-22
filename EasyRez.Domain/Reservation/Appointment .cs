using EasyRez.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Domain.Reservation
{
    public class Appointment:Entity<Guid>
    {
        public Guid CustomerId { get; private set; }
        public DateTime Date { get; private set; }
        public string? Notes { get; private set; }
        private readonly List<Service> _services = [];
        public IReadOnlyCollection<Service> Services => _services.AsReadOnly();

        private Appointment() { }
        private Appointment(Guid id, Guid customerId, DateTime date):base(id)
        {          
            CustomerId = customerId;
            Date = date;
        }

        public static Appointment Create(Guid customerId, DateTime date)
        {
            return new Appointment(Guid.NewGuid(), customerId, date);
        }
        public void AddNotes(string notes)
        {
            Notes = notes;
        }
        public void Update(Guid customerId, DateTime date, string notes)
        {
            CustomerId = customerId;
            Date = date;
            Notes = notes;
        }
       

    }
}
