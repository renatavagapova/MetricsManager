using MetricsAgent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public class SqlSettingsProvider : ISqlSettingsProvider
    {
        public string GetConnectionString()
        {
            return @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        }
    }
}
