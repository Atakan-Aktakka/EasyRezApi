using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Core.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent devent);
    }
}
