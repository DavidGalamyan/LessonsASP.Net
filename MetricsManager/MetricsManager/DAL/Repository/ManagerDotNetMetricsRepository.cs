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
    public class ManagerDotNetMetricsRepository : IManagerDotNetMetricsRepository
    {
        private readonly ISqlSettingsProvider _sqliteConnection;
        public ManagerDotNetMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(IList<DotNetMetric> listMetric)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                foreach (var metric in listMetric)
                {
                    connection.Execute($"INSERT INTO dotnetmetric(agentId, value, time) VALUES({metric.AgentId}, {metric.Value}, {metric.DateTime.ToUnixTimeSeconds()})");
                }
            }
        }

        public IList<DotNetMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetric WHERE (agentId==@agentId AND time>@fromTime AND time<@toTime)",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                        agentId = agentId
                    }).ToList();
            }
        }

        public IList<DotNetMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<DotNetMetric>("SELECT * FROM dotnetmetric WHERE (time>=@fromTime AND time<@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();
            }
        }

        public DotNetMetric GetLastDateTimeFromBase(int agentId)
        {
            try
            {
                using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
                {
                    return connection.QuerySingle<DotNetMetric>($"SELECT MAX(Time) FROM dotnetmetric WHERE agentId = {agentId}");
                }
            }
            catch (Exception)
            { return null; }
        }
    }
}
