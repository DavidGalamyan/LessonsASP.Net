using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public class ManagerHddMetricsRepository : IManagerHddMetricsRepository
    {
        public void Create(IList<HddMetric> listMetric)
        {
            throw new NotImplementedException();
        }

        public IList<HddMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agent)
        {
            throw new NotImplementedException();
        }

        public IList<HddMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public HddMetric GetLastDateTimeFromBase(int agentId)
        {
            throw new NotImplementedException();
        }
    }
}
