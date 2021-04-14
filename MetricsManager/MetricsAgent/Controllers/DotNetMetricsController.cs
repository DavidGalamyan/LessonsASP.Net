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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {        
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository,IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            _repository = repository;
        }


        [HttpGet("getmetric")]
        public IActionResult GetMetricsByTimeInterval([FromBody] MetricsFilterRequest dateTimeOffsetModel)
        {
            _logger.LogInformation($"GetMetricsByTimeInterval: fromTime {dateTimeOffsetModel.fromTime},toTime {dateTimeOffsetModel.toTime}");
            var metrics = _repository.GetByTimeInterval(dateTimeOffsetModel.fromTime, dateTimeOffsetModel.toTime);
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Ok(response);
        }

    }
}
