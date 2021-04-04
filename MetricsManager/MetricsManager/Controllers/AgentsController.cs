using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly List<AgentInfo> _agentsList;

        private readonly ILogger<AgentsController> _logger;

        public AgentsController(List<AgentInfo> agentsList, ILogger<AgentsController> logger) 
        {
            _logger = logger;
            _agentsList = agentsList;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"RefisterAgent:agentId {agentInfo.AgentId}, agentURI {agentInfo.AgentAddress}");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"EnableAgentById:agentId {agentId}");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"DisableAgentById:agentId {agentId}");
            return Ok();
        }

    }
}
