using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsTool.SQLiteConnectionSettings;

namespace MetricsManager.DAL.Repository
{
    public class ManagerCpuMetricsRepository : IManagerCpuMetricsRepository
    {
        private readonly ISqlSettingsProvider _sqliteConnection;
        public ManagerCpuMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
        }
        public void Create(IList<CpuMetric> listMetric)
        {
            throw new NotImplementedException();
        }

        public IList<CpuMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }
    }
}
