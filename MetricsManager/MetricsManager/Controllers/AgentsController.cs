using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Requests.ControllerRequests;
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
        private readonly ILogger<AgentsController> _logger;
        private IAgentInfoRepository _repository;

        public AgentsController(ILogger<AgentsController> logger, IAgentInfoRepository repository) 
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult RegisterAgent([FromBody] AgentInfoRequest agent)
        {
            _logger.LogInformation($"RefisterAgent:agentAddress {agent.AgentAddress}");
            if (_repository.RegisterAgent(agent))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Такой агент уже добавлен");
            }
        }

        // для удобства отладки
        [HttpGet]
        public IActionResult GetAllAgents()
        {
            IList<AgentInfo> agents = _repository.GetAllAgents();
            var response = new List<AgentInfo>();
            

            foreach (AgentInfo agent in agents)
            {
                response.Add(agent);
            }
            return Ok(response);
        }
    }
}
