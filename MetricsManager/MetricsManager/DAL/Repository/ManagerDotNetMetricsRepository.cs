using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public class ManagerDotNetMetricsRepository : IManagerDotNetMetricsRepository
    {
        public void Create(IList<DotNetMetric> listMetric)
        {
            throw new NotImplementedException();
        }

        public IList<DotNetMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agent)
        {
            throw new NotImplementedException();
        }

        public IList<DotNetMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public DotNetMetric GetLastDateTimeFromBase(int agentId)
        {
            throw new NotImplementedException();
        }
    }
}
