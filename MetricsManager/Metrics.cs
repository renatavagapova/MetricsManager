using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class Metrics
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }

        public Metrics(DateTime date, int temperature)
        {
            Date = date;
            Temperature = temperature;
        }
    }
}
