using EasyRez.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Domain.Reservation
{
    public class Service : Entity<Guid>
    {
        public string Name { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Description { get; private set; }
        public TimeSpan Duration { get; private set; }
        public TimeSpan? ReminderAfter { get; private set; } 
        private Service(){}
        private Service(Guid id, string name, Guid customerId, string description, TimeSpan duration, TimeSpan? reminderAfter):base(id)
        {
            Name = name;
            CustomerId = customerId;
            Description = description;
            Duration = duration;
        }
        public static Service Create(string name, Guid customerId, string description, TimeSpan duration, TimeSpan? reminderAfter)
        {
            return new Service(Guid.NewGuid(), name, customerId, description, duration, reminderAfter);
        }
        public void Update(string name, Guid customerId, string description, TimeSpan duration, TimeSpan? reminderAfter)
        {
            Name = name;
            CustomerId = customerId;
            Description = description;
            Duration = duration;
            ReminderAfter = reminderAfter;
        }
    }
}
