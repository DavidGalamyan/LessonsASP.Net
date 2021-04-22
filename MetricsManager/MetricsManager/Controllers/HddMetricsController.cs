using MetricsManager.DAL.Interfaces;
using MetricsManager.Requests;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IMetricsAgentClient agentClient, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            _agentClient = agentClient;
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/left")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation($"GetMetricsFromAgent:agentId {agentId}");
            return Ok();
        }
        [HttpGet("cluster/left")]
        public IActionResult GetMetricsFromAllCluster()
        {
            _logger.LogInformation($"GetMetricsFromAllCluster: OK");
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAllMetricsForAgent([FromBody] GetAllHddMetricsApiRequest request)
        {
            _logger.LogInformation($"GetAllMetricsForAgent: agentAddress {request.AgentAddress}, FromTime {request.FromTime}, ToTime {request.ToTime}");
            var metrics = _agentClient.GetAllHddMetrics(request);
            return Ok(metrics);
        }
        //var metrics = _repository.GetByTimeInterval(dateTimeOffsetModel.FromTime, dateTimeOffsetModel.ToTime);
        //var response = new AllHddMetricsResponse()
        //{
        //    Metrics = new List<HddMetricDto>()
        //};
        //    foreach (var metric in metrics)
        //    {
        //        response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
        //    }
        //    return Ok(response);
}
}
