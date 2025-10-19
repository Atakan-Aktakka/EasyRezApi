using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRez.Api.Workers
{
    public class WorkerSettings
    {
        public int IntervalInMinutes { get; set; } = 5;
        public int SchedulerTickIntervalInMinutes { get; set; } = 5;
    }
}