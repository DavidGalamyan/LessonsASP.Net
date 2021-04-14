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
    public class HddMetricsJob : IJob
    {
        private PerformanceCounter _hddCounter;
        private IHddMetricsRepository _repository;

        public HddMetricsJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("LogicalDisk", "Free Megabytes", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var localDiskFreeMemory = Convert.ToInt32(_hddCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new HddMetric() { Time = time, Value = localDiskFreeMemory });

            return Task.CompletedTask;
        }
    }
}
