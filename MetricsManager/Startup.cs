using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repository;
using MetricsManager.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using MetricsManager.DAL;
using Microsoft.OpenApi.Models;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IMetricsManagerClient, MetricsManagerClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ =>
                TimeSpan.FromMilliseconds(1000)));

            services.AddControllers();

            services.AddSingleton<ISqlSettingsProvider, SqlSettingsProvider>();
            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(new SqlSettingsProvider().GetConnectionString())
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricsFromAgents),
                cronExpression: "20/30 * * * * ?"));
            services.AddSingleton<DotNetMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsFromAgents),
                cronExpression: "20/30 * * * * ?"));
            services.AddSingleton<NetworkMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricsFromAgents),
                cronExpression: "20/30 * * * * ?"));
            services.AddSingleton<HddMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricsFromAgents),
                cronExpression: "20/30 * * * * ?"));
            services.AddSingleton<RamMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricsFromAgents),
                cronExpression: "20/30 * * * * ?"));
            services.AddHostedService<QuartzHostedService>();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ñåðâèñà àãåíòà ñáîðà ìåòðèê",
                    Description = "Òóò ìîæíî ïîèãðàòü ñ api íàøåãî ñåðâèñà",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kiverin",
                        Email = "kiverinda@yandex.ru",
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "ìîæíî óêàçàòü ïîä êàêîé ëèöåíçèåé âñå îïóáëèêîâàíî",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ñåðâèñà àãåíòà ñáîðà ìåòðèê");
            });
        }
    }
}