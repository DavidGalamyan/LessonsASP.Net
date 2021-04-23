using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IManagerRepository<T> where T : class
    {
        IList<T> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime);

        void Create(IList<T> listMetric);

        T GetLastDateTimeFromBase(int agentId);
    }

}
