using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricsJob : IJob
    {
        private PerformanceCounter _ramCounter;
        private IRamMetricsRepository _repository;

        public RamMetricsJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var usedRamMemory = Convert.ToInt32(_ramCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new RamMetric() { Time = time, Value = usedRamMemory });

            return Task.CompletedTask;
        }
    }
}
