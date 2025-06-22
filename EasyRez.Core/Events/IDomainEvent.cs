using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Core.Events
{
    public interface IDomainEvent
    {
        public DateTime DateOccured { get; set; }

        public string Serialize();
    }
}
