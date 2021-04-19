using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Requests.ControllerRequests
{
    public class AgentInfoRequest
    {
        public int AgentId { get; set; }

        public string AgentAddress { get; set; }
    }
}
