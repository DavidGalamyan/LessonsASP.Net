using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsTool.SQLiteConnectionSettings;
using Dapper;
using MetricsTool;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repository
{
    public class ManagerCpuMetricsRepository : IManagerCpuMetricsRepository
    {
        private readonly ISqlSettingsProvider _sqliteConnection;
        public ManagerCpuMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(IList<CpuMetric> listMetric)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                foreach (var metric in listMetric)
                {
                    connection.Execute($"INSERT INTO cpumetrics(agentId, value, time) VALUES({metric.AgentId}, {metric.Value}, {metric.DateTime.ToUnixTimeSeconds()})");
                }
            }
        }

        public IList<CpuMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public CpuMetric GetLastDateTimeFromBase(int agentId)
        {
            try
            {
                using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
                {
                    return connection.QuerySingle<CpuMetric>($"SELECT MAX(Time) FROM cpumetrics WHERE agentId = {agentId}");
                }
            }
            catch (Exception)
            { return null; }
        }
    }
}
