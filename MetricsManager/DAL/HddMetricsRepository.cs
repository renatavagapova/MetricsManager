using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetricModel>
    {
        IList<HddMetricModel> GetMetricsFromTimeToTime(TimeSpan fromTime, TimeSpan toTime);
        IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgent(TimeSpan fromTime, TimeSpan toTime, int idAgent);
        IList<HddMetricModel> GetMetricsFromTimeToTimeOrderBy(TimeSpan fromTime, TimeSpan toTime, string sortingField);
        IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(TimeSpan fromTime, TimeSpan toTime, string sortingField, int idAgent);
    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private SQLiteConnection _connection;
        public HddMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(HddMetricModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"INSERT INTO hddmetrics(idagent, value, time) VALUES({item.Value}, {item.Time.TotalSeconds}, {item.IdAgent})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"DELETE FROM hddmetrics WHERE id = {id}";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(HddMetricModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"UPDATE hddmetrics SET value = {item.Value}, time = {item.Time.TotalSeconds}, idagent = {item.IdAgent} WHERE id = {item.Id}";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<HddMetricModel> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM hddmetrics";
            var returnList = new List<HddMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public HddMetricModel GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE id = {id}";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new HddMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTime(TimeSpan fromTime, TimeSpan toTime)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds}";
            var returnList = new List<HddMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgent(TimeSpan fromTime, TimeSpan toTime, int idAgent)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} AND idagent = {idAgent}";
            var returnList = new List<HddMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTimeOrderBy(TimeSpan fromTime, TimeSpan toTime, string field)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} ORDER BY {field}";
            var returnList = new List<HddMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }

        public IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(TimeSpan fromTime, TimeSpan toTime, string field, int idAgent)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText =
                $"SELECT * FROM hddmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} AND idagent = {idAgent} ORDER BY {field}";
            var returnList = new List<HddMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricModel
                    {
                        Id = reader.GetInt32(0),
                        IdAgent = reader.GetInt32(1),
                        Value = reader.GetDouble(2),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(3))
                    });
                }
            }
            return returnList;
        }
    }
}

