using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public class ManagerNetworkMetricsRepository : IManagerNetworkMetricsRepository
    {
        public void Create(IList<NetworkMetric> listMetric)
        {
            throw new NotImplementedException();
        }

        public IList<NetworkMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public NetworkMetric GetLastDateTimeFromBase(int agentId)
        {
            throw new NotImplementedException();
        }
    }
}
