using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricsJob : IJob
    {
        private PerformanceCounter _dotNetCounter;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricsJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //
            var clrMemory = Convert.ToInt32(_dotNetCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new DotNetMetric { Time = time, Value = clrMemory });

            return Task.CompletedTask;
        }
    }
}
