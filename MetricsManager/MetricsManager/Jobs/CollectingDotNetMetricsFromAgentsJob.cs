using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Jobs.PropertieJob;
using MetricsManager.Requests;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class CollectingDotNetMetricsFromAgentsJob : IJob
    {
        private readonly IAgentInfoRepository _agentRepository;
        private readonly IManagerDotNetMetricsRepository _repository;
        private readonly IMetricsAgentClient _metricsAgentClient;
        private readonly IMapper _mapper;
        public CollectingDotNetMetricsFromAgentsJob(IAgentInfoRepository agentRepository, IManagerDotNetMetricsRepository repository,
                                                    IMetricsAgentClient metricsAgentClient, IMapper mapper)
        {
            _agentRepository = agentRepository;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _mapper = mapper;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var agentList = _agentRepository.GetAllAgents();
            foreach (var agent in agentList)
            {
                var lastRecordTime = UsefulMethod.CheckDateTime(_repository.GetLastDateTimeFromBase(agent.AgentId).DateTime);
                var request = new GetAllDotNetMetricsApiRequest()
                {
                    AgentAddress = agent.AgentAddress,
                    FromTime = lastRecordTime.DateTime,
                    ToTime = DateTime.UtcNow
                };
                var response = _metricsAgentClient.GetAllDotNetMetrics(request);
                if (response.Metrics != null && response != null)
                {
                    var metricList = new List<DotNetMetric>();
                    foreach (var metric in response.Metrics)
                    {
                        var convertedMetric = _mapper.Map<DotNetMetric>(metric);
                        convertedMetric.AgentId = agent.AgentId;
                        metricList.Add(convertedMetric);
                    }
                    _repository.Create(metricList);
                }
            }
            return Task.CompletedTask;
        }
    }
}
