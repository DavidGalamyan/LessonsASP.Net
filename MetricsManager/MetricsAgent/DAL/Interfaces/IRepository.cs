using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime);

        void Create(T item);
    }
}
