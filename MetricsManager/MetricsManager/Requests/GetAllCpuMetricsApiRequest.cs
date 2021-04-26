using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Requests
{
    public class GetAllCpuMetricsApiRequest
    {
        public DateTime FromTime { get; set; }
        
        public DateTime ToTime { get; set; }

        public string AgentAddress { get; set; }
    }
}
