using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricModel>
    {
        IList<DotNetMetricModel> GetMetricsFromTimeToTime(TimeSpan fromTime, TimeSpan toTime);
        IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgent(TimeSpan fromTime, TimeSpan toTime, int idAgent);
        IList<DotNetMetricModel> GetMetricsFromTimeToTimeOrderBy(TimeSpan fromTime, TimeSpan toTime, string sortingField);
        IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(TimeSpan fromTime, TimeSpan toTime, string sortingField, int idAgent);
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private SQLiteConnection _connection;
        public DotNetMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(DotNetMetricModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"INSERT INTO dotnetmetrics(idagent, value, time) VALUES({item.Value}, {item.Time.TotalSeconds}, {item.IdAgent})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"DELETE FROM dotnetmetrics WHERE id = {id}";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(DotNetMetricModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"UPDATE dotnetmetrics SET value = {item.Value}, time = {item.Time.TotalSeconds}, idagent = {item.IdAgent} WHERE id = {item.Id}";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<DotNetMetricModel> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics";
            var returnList = new List<DotNetMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public DotNetMetricModel GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE id = {id}";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new DotNetMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTime(TimeSpan fromTime, TimeSpan toTime)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds}";
            var returnList = new List<DotNetMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgent(TimeSpan fromTime, TimeSpan toTime, int idAgent)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} AND idagent = {idAgent}";
            var returnList = new List<DotNetMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeOrderBy(TimeSpan fromTime, TimeSpan toTime, string field)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} ORDER BY {field}";
            var returnList = new List<DotNetMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(TimeSpan fromTime, TimeSpan toTime, string field, int idAgent)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText =
                $"SELECT * FROM dotnetmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} AND idagent = {idAgent} ORDER BY {field}";
            var returnList = new List<DotNetMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }
    }
}
