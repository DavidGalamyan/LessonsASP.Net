﻿using AutoMapper;
using MetricsAgent.DAL.Interface;
using MetricsAgent.DAL.Model;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation($"CreateRamMetric (Request.Time :{request.Time}, Request.Value:{request.Value})");
            _repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }

        [HttpGet("getmetric")]
        public IActionResult GetMetricsByTimeInterval([FromBody] MetricsFilterRequest dateTimeOffsetModel)
        {
            _logger.LogInformation($"GetMetricsByTimeInterval: fromTime {dateTimeOffsetModel.fromTime},toTime {dateTimeOffsetModel.toTime}");
            var metrics = _repository.GetByTimeInterval(dateTimeOffsetModel.fromTime, dateTimeOffsetModel.toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            return Ok(response);
        }

        [HttpGet("available")]
        public IActionResult GetMetrics()
        {
            _logger.LogInformation($"GetMetricsFromAllCluster: AllCluster");
            return Ok();
        }
    }
}
