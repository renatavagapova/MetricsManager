using MetricsManager.DAL.Interfaces;

namespace MetricsManager.DAL
{
    public class SqlSettingsProvider : ISqlSettingsProvider
    {
        public string GetConnectionString()
        {
            return @"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        }
    }
}