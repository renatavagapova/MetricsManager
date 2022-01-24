using MetricsManager.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IApplicationBuilder app)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<IAgentsRepository, AgentsRepository>();
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsManager v1"));
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            string connectionString = "Data Source=:memory:";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddSingleton(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS agents";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE agents(
                    id INTEGER PRIMARY KEY, 
                    status INT, 
                    ipaddress STRING, 
                    name STRING)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO agents(status, ipaddress, name) VALUES(1, '192.168.1.1:20359', 'user1');";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO agents(status, ipaddress, name) VALUES(0, '192.168.1.2:20359', 'user2')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO agents(status, ipaddress, name) VALUES(1, '192.168.1.3:20359', 'user3')";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE cpumetrics(
                    id INTEGER PRIMARY KEY, 
                    idagent INT, 
                    value INT, 
                    time INT,
                    FOREIGN KEY(idagent) REFERENCES agents(id))";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(idagent, value, time) VALUES(1, 40, 15)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(idagent, value, time) VALUES(2, 10, 8)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(idagent, value, time) VALUES(1, 18, 20)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE dotnetmetrics(
                    id INTEGER PRIMARY KEY, 
                    idagent INT, 
                    value INT, 
                    time INT,
                    FOREIGN KEY(idagent) REFERENCES agents(id))";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(idagent, value, time) VALUES(1, 40, 15)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(idagent, value, time) VALUES(2, 34, 8)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(idagent, value, time) VALUES(1, 18, 20)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE hddmetrics(
                    id INTEGER PRIMARY KEY, 
                    idagent INT, 
                    value DOUBLE, 
                    time INT,
                    FOREIGN KEY(idagent) REFERENCES agents(id))";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time, idagent) VALUES(1520298.6526352, 12, 1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time, idagent) VALUES(1264.23456987, 23, 3)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value, time, idagent) VALUES(544321.5432234, 34, 1)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE rammetrics(
                    id INTEGER PRIMARY KEY, 
                    idagent INT, 
                    value DOUBLE, 
                    time INT,
                    FOREIGN KEY(idagent) REFERENCES agents(id))";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time, idagent) VALUES(152.652, 11, 1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time, idagent) VALUES(1264.234, 27, 2)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value, time, idagent) VALUES(54.543, 32, 1)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE networkmetrics(
                    id INTEGER PRIMARY KEY, 
                    idagent INT, 
                    value INT, 
                    time INT,
                    FOREIGN KEY(idagent) REFERENCES agents(id))";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time, idagent) VALUES(140, 115, 1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time, idagent) VALUES(210, 28, 3)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time, idagent) VALUES(318, 220, 1)";
                command.ExecuteNonQuery();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsManager v1"));
            }
        }
    }
}
