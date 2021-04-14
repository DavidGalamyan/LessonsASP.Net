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
    public class NetworkMetricsJob : IJob
    {
        private PerformanceCounter _networkCounter;
        private INetworkMetricsRepository _repository;
         
        public NetworkMetricsJob (INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Adapter", "Bytes Received/sec" , "Realtek PCIe 2.5GbE Family Controller");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkSpeed = Convert.ToInt32(_networkCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new NetworkMetric() { Time = time, Value = networkSpeed });

            return Task.CompletedTask;
        }
    }
}
