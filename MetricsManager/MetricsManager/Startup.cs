using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MetricsManager.Jobs.ProperieJob;
using MetricsTool.SQLiteConnectionSettings;
using MetricsManager.DAL.Repository;
using MetricsManager.DAL.Interfaces;
using Polly;
using System;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using MetricsManager.Jobs;
using AutoMapper;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string _connectionSQLite = new SqlSettingsProvider().GetConnectionSQLite();
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // добавляем поддержку SQLite 
                    .AddSQLite()
                    // устанавливаем строку подключения
                    .WithGlobalConnectionString(_connectionSQLite)
                    // подсказываем где искать классы с миграциями
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());            
            services.AddControllers();

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddSingleton<ISqlSettingsProvider, SqlSettingsProvider>();
            services.AddSingleton<IAgentInfoRepository, AgentInfoRepository>();
            services.AddSingleton<IManagerCpuMetricsRepository, ManagerCpuMetricsRepository>();
            services.AddSingleton<IManagerDotNetMetricsRepository, ManagerDotNetMetricsRepository>();
            services.AddSingleton<IManagerHddMetricsRepository, ManagerHddMetricsRepository>();
            services.AddSingleton<IManagerNetworkMetricsRepository, ManagerNetworkMetricsRepository>();
            services.AddSingleton<IManagerRamMetricsRepository, ManagerRamMetricsRepository>();

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // добавляем нашу задачу
            services.AddSingleton<CollectingCpuMetricsFromAgentsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(CollectingCpuMetricsFromAgentsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<CollectingDotNetMetricsFromAgentsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(CollectingDotNetMetricsFromAgentsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<CollectingHddMetricsFromAgentsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(CollectingHddMetricsFromAgentsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<CollectingNetworkMetricsFromAgentsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(CollectingNetworkMetricsFromAgentsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<CollectingRamMetricsFromAgentsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(CollectingRamMetricsFromAgentsJob),
                cronExpression: "0/5 * * * * ?"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
