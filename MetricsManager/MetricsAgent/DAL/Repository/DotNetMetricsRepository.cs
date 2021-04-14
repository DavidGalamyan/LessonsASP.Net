﻿using Dapper;
using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private ISqlSettingsProvider _sqliteConnection;

        public DotNetMetricsRepository(ISqlSettingsProvider sqliteConnection)
        {
            // добавляем парсилку типа DateTimeOffset в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            _sqliteConnection = sqliteConnection;
        }
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
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

            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
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
