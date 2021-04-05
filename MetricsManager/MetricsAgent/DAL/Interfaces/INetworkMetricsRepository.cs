using MetricsAgent.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interface
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {

    }
}
