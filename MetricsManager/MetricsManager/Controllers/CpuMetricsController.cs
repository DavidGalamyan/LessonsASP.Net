using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsTool;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"GetMetricsFromAgent:agentId {agentId},fromTime {fromTime},toTime {toTime}");
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:50343/api/cpumetrics/from/1/to/999999");
            //request.Headers.Add("Accept", "application/vnd.github.v3+json");
            //var client = clientFactory.CreateClient();
            //HttpResponseMessage response = client.SendAsync(request).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    using var responseStream = response.Content.ReadAsStreamAsync().Result;
            //    var metricsResponse = JsonSerializer.DeserializeAsync
            //        <CpuMetricsResponse>(responseStream).Result;
            //}
            //else
            //{
            //    // ошибка при получении ответа
            //}
            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"GetMetricsByPercentileFromAgent:agentId {agentId},fromTime {fromTime},toTime {toTime},Percentile {percentile}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"GetMetricsFromAllCluster:fromTime {fromTime}, toTime {toTime}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"GetMetricsByPercentileFromAllCluster:fromTime {fromTime}, toTime {toTime}, Percenrile {percentile}");
            return Ok();
        }
    }
}
