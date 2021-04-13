using MetricsAgent.DAL.Interface;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;

        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;            
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
