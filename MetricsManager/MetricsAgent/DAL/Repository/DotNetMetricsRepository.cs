using Dapper;
using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = @"Data Source=metrics.db; Version=3; Pooling=True;Max Poll Size=100;";

        public DotNetMetricsRepository()
        {
            // добавляем парсилку типа DateTimeOffsetHandler в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
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

        public IList<DotNetMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {

            using (var conncetion = new SQLiteConnection(ConnectionString))
            {
                return conncetion.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE (Time>=@fromTime AND Time<=@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();
            }
        }
    }
}
