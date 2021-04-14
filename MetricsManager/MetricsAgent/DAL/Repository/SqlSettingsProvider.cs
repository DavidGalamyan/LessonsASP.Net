using MetricsAgent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repository
{
    public class SqlSettingsProvider : ISqlSettingsProvider
    {
        private const string _connectionString = @"Data Source=metrics.db;Version=3;Pooling=True;Max Pool Size=100;";

        public string GetConnectionSQLite()
        {
            return _connectionString;
        }
    }
}
