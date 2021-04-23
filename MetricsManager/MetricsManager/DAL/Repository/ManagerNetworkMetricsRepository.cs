using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsTool;
using MetricsTool.SQLiteConnectionSettings;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    public class ManagerNetworkMetricsRepository : IManagerNetworkMetricsRepository
    {
        private readonly ISqlSettingsProvider _sqliteConnection;
        public ManagerNetworkMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(IList<NetworkMetric> listMetric)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                foreach (var metric in listMetric)
                {
                    connection.Execute($"INSERT INTO networkmetrics (agentId, value, time) VALUES({metric.AgentId}, {metric.Value}, {metric.DateTime.ToUnixTimeSeconds()})");
                }
            }
        }

        public IList<NetworkMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (agentId==@agentId AND time>@fromTime AND time<@toTime)",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                        agentId = agentId
                    }).ToList();
            }
        }

        public IList<NetworkMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time>=@fromTime AND time<@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();
            }
        }

        public NetworkMetric GetLastDateTimeFromBase(int agentId)
        {
            try
            {
                using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
                {
                    return connection.QuerySingle<NetworkMetric>($"SELECT MAX(Time) FROM networkmetrics WHERE agentId = {agentId}");
                }
            }
            catch (Exception)
            { return null; }
        }
    }
}
