using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repository
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly SQLiteConnection _connection;
        public RamMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RamMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("INSERT INTO rammetrics (available, time) VALUES(@available, @time)",
                new
                {
                    available = item.Available,
                    time = item.Time
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(RamMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("UPDATE rammetrics SET available = @available, time = @time WHERE id = @id",
                new
                {
                    available = item.Available,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<RamMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<RamMetricModel>($"SELECT id, time, available From rammetrics")
                .ToList();
        }

        public RamMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .QuerySingle<RamMetricModel>("SELECT id, time, available FROM rammetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }
    }
}
