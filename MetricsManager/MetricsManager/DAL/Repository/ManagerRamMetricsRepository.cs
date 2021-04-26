using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsTool;
using MetricsTool.SQLiteConnectionSettings;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repository
{
    public class ManagerRamMetricsRepository : IManagerRamMetricsRepository
    {
        private readonly ISqlSettingsProvider _sqliteConnection;
        public ManagerRamMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(IList<RamMetric> listMetric)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                foreach (var metric in listMetric)
                {
                    connection.Execute($"INSERT INTO rammetrics (agentId, value, time) VALUES({metric.AgentId}, {metric.Value}, {metric.DateTime.ToUnixTimeSeconds()})");
                }
            }
        }

        public IList<RamMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE (agentId==@agentId AND time>@fromTime AND time<@toTime)",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                        agentId = agentId
                    }).ToList();
            }
        }

        public IList<RamMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<RamMetric>("SELECT * FROM rammetrics WHERE (time>=@fromTime AND time<@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();
            }
        }

        public RamMetric GetLastDateTimeFromBase(int agentId)
        {
            try
            {
                using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
                {
                    return connection.QuerySingle<RamMetric>($"SELECT MAX(Time) FROM rammetrics WHERE agentId = {agentId}");
                }
            }
            catch (Exception)
            { return null; }
        }
    }
}
