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

        public IList<CpuMetric> GetByAgentTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime, int agentId)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (agentId==@agentId AND time>@fromTime AND time<@toTime)",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds(),
                        agentId = agentId
                    }).ToList();
            }
        }

        public IList<CpuMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time>=@fromTime AND time<@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();
            }
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
