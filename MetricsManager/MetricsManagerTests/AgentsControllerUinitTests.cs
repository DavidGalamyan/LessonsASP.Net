//using MetricsManager;
//using MetricsManager.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System.Collections.Generic;
//using Xunit;


//namespace MetricsManagerTests
//{
//    public class AgentsControllerUinitTests
//    {
//        private AgentsController _controller;
//        private Mock<ILogger<AgentsController>> _logger;
//        private readonly List<AgentInfo> _agentsList;

//        public AgentsControllerUinitTests()
//        {
//            _logger = new Mock<ILogger<AgentsController>>();
//            _agentsList = new List<AgentInfo>();
//            _controller = new AgentsController(_agentsList, _logger.Object);
//        }

//        [Fact]
//        public void RegisterAgent_ReturnsOk()
//        {
//            var agentId = new AgentInfo();

//            var result = _controller.RegisterAgent(agentId);

//            _ = Assert.IsAssignableFrom<IActionResult>(result);
//        }

//        [Fact]
//        public void EnableAgentById_ReturnsOk()
//        {
//            var agentId = 1;

//            var result = _controller.EnableAgentById(agentId);

//            _ = Assert.IsAssignableFrom<IActionResult>(result);
//        }

//        [Fact]
//        public void DisableAgentById_ReturnsOk()
//        {
//            var agentId = 1;

//            var result = _controller.DisableAgentById(agentId);

//             _ = Assert.IsAssignableFrom<IActionResult>(result);
//        }
//    }
//}
