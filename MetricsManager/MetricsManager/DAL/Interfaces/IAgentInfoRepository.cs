using MetricsManager.DAL.Models;
using MetricsManager.Requests.ControllerRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgentInfoRepository
    {
        public bool RegisterAgent(AgentInfoRequest agentInfoRequest);

        IList<AgentInfo> GetAllAgents();
    }
}
