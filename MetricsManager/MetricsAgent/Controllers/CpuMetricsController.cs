using AutoMapper;
using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsTool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _mapper = mapper;
        }


        [HttpGet("getmetric")]
        public IActionResult GetMetricsByTimeInterval([FromBody] MetricsFilterRequest dateTimeOffset)
        {
            _logger.LogInformation($"GetMetricsByTimeInterval: fromTime {dateTimeOffset.fromTime},toTime {dateTimeOffset.toTime}");
            var metrics = _repository.GetByTimeInterval(dateTimeOffset.fromTime, dateTimeOffset.toTime);
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Ok(response);           
        }
    }
}
