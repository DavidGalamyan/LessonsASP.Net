using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;

namespace MetricsAgent.DAL.Interface
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        //connect on base
        private SQLiteConnection _connection;
        // инжектируем соединение с базой данных в репозиторий через конструктор
        public CpuMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(CpuMetric item)
        {
            //create command
            using var cmd = new SQLiteCommand(_connection);
            // SQL command Insert data base
            cmd.CommandText = "INSERT INTO cpumetrics(value,time) VALUES(@value,@time)";
            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            // подготовка команды к выполнению
            cmd.Prepare();
            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            // cmd delete data
            cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetric> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);

            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                //читаем пока есть что читать из базы
                while (reader.Read())
                {
                    //добавляем объект в список возврата
                    returnList.Add(new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        //преобразуем прочитанные секунды в метку времени
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }

        public CpuMetric GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // если удалось что-то найти (прочитать)
                if (reader.Read())
                {
                    //return value
                    return new CpuMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    };
                }
                else return null;
            }
        }

        public void Update(CpuMetric item)
        {
            using var cmd = new SQLiteCommand(_connection);
            //прописываем команду SQL на обнавление данных
            cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
