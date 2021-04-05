using Dapper;
using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = "@Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size = 100;";

        NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO networkmetric (value,time) VALUES (@value,@time",
                    new { 
                        value = item.Value, 
                        time = item.Time.TotalSeconds 
                    });
            }
        }

        public IList<NetworkMetric> GetAll()
        {
           using (var connection = new SQLiteConnection(ConnectionString))
           {
                return connection.Query<NetworkMetric>("SELECT Id,Time,Value FROM networkmetrics").ToList();
           }
            
        }

        public NetworkMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<NetworkMetric>("SELECT Id,Time,Value FROM networkmetrics WHERE id=@id",
                    new { id = id });
            }
        }
    }
}
