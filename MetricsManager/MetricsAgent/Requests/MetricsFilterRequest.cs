using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Requests
{
    public class MetricsFilterRequest
    {
        public DateTimeOffset fromTime { get; set; }

        public DateTimeOffset toTime { get; set; }
    }
}
