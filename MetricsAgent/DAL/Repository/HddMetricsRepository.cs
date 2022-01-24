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
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public HddMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("INSERT INTO hddmetrics (freesize, time) VALUES(@freesize, @time)",
                new
                {
                    freesize = item.FreeSize,
                    time = item.Time
                });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("DELETE FROM hddmetrics WHERE id=@id",
                new
                {
                    id = target
                });
        }

        public void Update(HddMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("UPDATE hddmetrics SET freesize = @freesize, time = @time WHERE id = @id",
                new
                {
                    freesize = item.FreeSize,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<HddMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<HddMetricModel>($"SELECT id, time, freesize From hddmetrics")
                .ToList();
        }

        public HddMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .QuerySingle<HddMetricModel>("SELECT id, time, freesize FROM hddmetrics WHERE id = @id",
                    new
                    {
                        id = target
                    });
        }
    }
}
