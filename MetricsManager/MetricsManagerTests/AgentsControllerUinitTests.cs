using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUinitTests
    {
        private AgentsController _controller;

        private readonly List<AgentInfo> _agentsList;

        public AgentsControllerUinitTests()
        {
            _controller = new AgentsController(_agentsList);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var agentId = new AgentInfo();

            var result = _controller.RegisterAgent(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.DisableAgentById(agentId);

             _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
