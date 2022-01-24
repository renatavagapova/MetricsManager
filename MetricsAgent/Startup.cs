using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IApplicationBuilder app)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddSingleton(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using var command = new SQLiteCommand(connection);
            command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(40, 1617408500)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10, 1617401000)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(18, 1615008000)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(1, 161740100)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(3, 1615008000)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(0, 1617408500)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, freesize DOUBLE, time INT64)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO hddmetrics(freesize, time) VALUES(1520298.6526352, 161740100)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO hddmetrics(freesize, time) VALUES(1264.23456987, 1615008000)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO hddmetrics(freesize, time) VALUES(544321.5432234, 1617408500)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS rammetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, available DOUBLE, time INT64)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO rammetrics(available, time) VALUES(152.652, 164240100)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO rammetrics(available, time) VALUES(1264.234, 131740100)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO rammetrics(available, time) VALUES(54.543, 143240100)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(140, 1617408500)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(210, 1613208500)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(318, 1017408500)";
            command.ExecuteNonQuery();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }
        }
    }
}
