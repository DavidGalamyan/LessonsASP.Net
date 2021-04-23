using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.DAL.Interface;
using MetricsTool.SQLiteConnectionSettings;
using MetricsAgent.DAL.Repository;
using MetricsAgent.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Microsoft.OpenApi.Models;
using System;

namespace MetricsAgent
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

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // ��������� ��������� SQLite 
                    .AddSQLite()
                    // ������������� ������ �����������
                    .WithGlobalConnectionString(_connectionSQLite)
                    // ������������ ��� ������ ������ � ����������
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddControllers();
            services.AddSingleton<ISqlSettingsProvider, SqlSettingsProvider>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
                       
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<CpuMetricsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(CpuMetricsJob),
                cronExpression: "0/5 * * * * ?"));           // ��������� ������ 5 ������
            services.AddSingleton<DotNetMetricsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(DotNetMetricsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<HddMetricsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(HddMetricsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<NetworkMetricsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(NetworkMetricsJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<RamMetricsJob>();
            services.AddSingleton(new JobScheduleDto(
                jobType: typeof(RamMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ������� ������ ����� ������",
                    Description = "��� ����� �������� � api ������ �������",
                    TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "David",
                        Email = string.Empty,
                        Url = new Uri(""),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "����� ������� ��� ����� ��������� ��� ������������",
                        Url = new Uri(""),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // ��������� middleware � �������� ��� ��������� Swagger ��������.
            app.UseSwagger();
            // ��������� middleware ��� ��������� swagger-ui 
            // ��������� Swagger JSON �������� (���� ���������� �� ��������������� �������������
            // �� ������� ����� �������� UI).
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ������� ������ ����� ������");
            });
        }
    }
}
