using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class MetricsRepository
    {
        private List<Metrics> _metrics;

        public MetricsRepository()
        {
            _metrics = new List<Metrics>();
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-12T12:11:12"), 12));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-13T12:11:12"), 15));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-13T11:11:12"), 3));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-14T15:11:12"), -6));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-14T16:11:12"), 2));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-14T17:11:12"), 8));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-14T18:11:12"), 9));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-15T12:11:12"), -5));
            _metrics.Add(new Metrics(DateTime.Parse("2021-12-15T13:11:12"), -13));
        }

        public List<Metrics> Read(DateTime fromDate, DateTime toDate)
        {
            List<Metrics> list = new List<Metrics>();
            foreach (Metrics m in _metrics)
            {
                if (m.Date >= fromDate && m.Date <= toDate) list.Add(m);
            }
            return list;
        }

        public void Add(DateTime date, int temperature)
        {
            _metrics.Add(new Metrics(date, temperature));
        }

        public void Delete(DateTime fromDate, DateTime toDate)
        {
            for (int i = 0; i < _metrics.Count; i++)
            {
                if (_metrics[i].Date >= fromDate && _metrics[i].Date <= toDate)
                {
                    _metrics.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Update(DateTime date, int temperature)
        {
            foreach (Metrics m in _metrics)
            {
                if (m.Date == date) m.Temperature = temperature;
            }
        }
    }
}
