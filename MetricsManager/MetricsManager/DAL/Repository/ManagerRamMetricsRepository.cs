using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public class ManagerRamMetricsRepository : IManagerRamMetricsRepository
    {
        public void Create(IList<RamMetric> listMetric)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agent)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public RamMetric GetLastDateTimeFromBase(int agentId)
        {
            throw new NotImplementedException();
        }
    }
}
