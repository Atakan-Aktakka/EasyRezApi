using EasyRez.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Application.Common.Events
{
    public class DomainEventNotification<T> : INotification where T : DomainEvent
    {
        public T DomainEvent { get; }

        public DomainEventNotification(T domainEvent)
        {
            DomainEvent = domainEvent;
        }

    }
}
