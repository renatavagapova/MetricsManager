using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.DAL
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricModel>
    {
        IList<NetworkMetricModel> GetMetricsFromTimeToTime(TimeSpan fromTime, TimeSpan toTime);
        IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgent(TimeSpan fromTime, TimeSpan toTime, int idAgent);
        IList<NetworkMetricModel> GetMetricsFromTimeToTimeOrderBy(TimeSpan fromTime, TimeSpan toTime, string sortingField);
        IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(TimeSpan fromTime, TimeSpan toTime, string sortingField, int idAgent);
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private SQLiteConnection _connection;
        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(NetworkMetricModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"INSERT INTO networkmetrics(idagent, value, time) VALUES({item.Value}, {item.Time.TotalSeconds}, {item.IdAgent})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"DELETE FROM networkmetrics WHERE id = {id}";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(NetworkMetricModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"UPDATE networkmetrics SET value = {item.Value}, time = {item.Time.TotalSeconds}, idagent = {item.IdAgent} WHERE id = {item.Id}";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<NetworkMetricModel> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM networkmetrics";
            var returnList = new List<NetworkMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricModel
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

        public NetworkMetricModel GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE id = {id}";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new NetworkMetricModel
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

        public IList<NetworkMetricModel> GetMetricsFromTimeToTime(TimeSpan fromTime, TimeSpan toTime)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds}";
            var returnList = new List<NetworkMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricModel
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

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgent(TimeSpan fromTime, TimeSpan toTime, int idAgent)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} AND idagent = {idAgent}";
            var returnList = new List<NetworkMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricModel
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

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeOrderBy(TimeSpan fromTime, TimeSpan toTime, string field)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} ORDER BY {field}";
            var returnList = new List<NetworkMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricModel
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

        public IList<NetworkMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(TimeSpan fromTime, TimeSpan toTime, string field, int idAgent)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText =
                $"SELECT * FROM networkmetrics WHERE time > {fromTime.TotalSeconds} AND time < {toTime.TotalSeconds} AND idagent = {idAgent} ORDER BY {field}";
            var returnList = new List<NetworkMetricModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricModel
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
