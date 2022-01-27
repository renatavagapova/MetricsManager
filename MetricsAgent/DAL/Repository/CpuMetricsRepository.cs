using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repository
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly SQLiteConnection _connection;

        public CpuMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
            new
            {
                value = item.Value,
                time = item.Time.ToUnixTimeSeconds()
            });
        }

        public void Delete(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
            new
            {
                id = target
            });
        }

        public void Update(CpuMetricModel item)
        {
            using var connection = new SQLiteConnection(_connection);
            connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
            new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<CpuMetricModel> GetAll()
        {
            using var connection = new SQLiteConnection(_connection);
            var q = connection
                .Query<CpuMetricModel>($"SELECT id, time, value From cpumetrics")
                .ToList();
            return q;
        }

        public CpuMetricModel GetById(int target)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .QuerySingle<CpuMetricModel>("SELECT Id, Time, Value FROM cpumetrics WHERE id = @id",
            new
            {
                id = target
            });
        }

        public IList<CpuMetricModel> GetMetricsFromTimeToTime(
            DateTimeOffset fromTime,
            DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<CpuMetricModel>(
                    $"SELECT id, time, value From cpumetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }

        public IList<CpuMetricModel> GetMetricsFromTimeToTimeOrderBy(
            DateTimeOffset fromTime,
            DateTimeOffset toTime,
            string sortingField)
        {
            using var connection = new SQLiteConnection(_connection);
            return connection
                .Query<CpuMetricModel>($"SELECT * FROM cpumetrics WHERE time > @fromTime AND time < @toTime ORDER BY {sortingField}",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    })
                .ToList();
        }
    }
}
