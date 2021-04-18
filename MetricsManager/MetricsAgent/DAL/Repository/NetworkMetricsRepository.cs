using Dapper;
using MetricsAgent.DAL.Interface;
using MetricsTool.SQLiteConnectionSettings;
using MetricsAgent.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private ISqlSettingsProvider _sqliteConnection;

        public NetworkMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            // добавляем парсилку типа DateTimeOffset в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {

            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (Time>=@fromTime AND Time<=@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();
            }
        }
    }
}
