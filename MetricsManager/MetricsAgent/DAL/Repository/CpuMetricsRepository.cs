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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // строка подключения
        private ISqlSettingsProvider _sqliteConnection;

        //инжектируем соединение с базой данных в наш репозиторий через конструктор
        public CpuMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            // добавляем парсилку типа DateTimeOffset в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time.ToUnixTimeSeconds()
                    }) ;
            }
        }

        public IList<CpuMetric> GetByTimeInterval(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            
            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time>=@fromTime AND time<=@toTime)",
                  new
                  {
                      fromTime = fromTime.ToUnixTimeSeconds(),
                      toTime = toTime.ToUnixTimeSeconds()
                  }).ToList();                    
            }
        }
    }
}

