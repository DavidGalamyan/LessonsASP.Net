using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsTool.SQLiteConnectionSettings;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class CollectingCpuMetricsFromAgentsJob : IJob
    {
        private ISqlSettingsProvider _sqliteConnection;
        private IAgentInfoRepository _agentRepository;
        private IManagerCpuMetricsRepository _repository;
        public CollectingCpuMetricsFromAgentsJob(ISqlSettingsProvider sqliteConnection, IAgentInfoRepository agentRepository, IManagerCpuMetricsRepository repository)
        {
            _sqliteConnection = sqliteConnection;
            _agentRepository = agentRepository;
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
