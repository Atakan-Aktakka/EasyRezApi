using EasyRez.Core.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Core
{
    public class EntityBase
    {
        protected readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();
        public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent @eventItem)
        {
            _domainEvents.Enqueue(@eventItem);
        }

        protected void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
