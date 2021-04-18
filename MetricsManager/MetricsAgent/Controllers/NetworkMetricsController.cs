using AutoMapper;
using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger,INetworkMetricsRepository repository,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }


        [HttpGet]
        public IActionResult GetMetricsByTimeInterval([FromBody] MetricsFilterRequest dateTimeOffsetModel)
        {
            _logger.LogInformation($"GetMetricsByTimeInterval: fromTime {dateTimeOffsetModel.FromTime},toTime {dateTimeOffsetModel.ToTime}");
            var metrics = _repository.GetByTimeInterval(dateTimeOffsetModel.FromTime, dateTimeOffsetModel.ToTime);
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
