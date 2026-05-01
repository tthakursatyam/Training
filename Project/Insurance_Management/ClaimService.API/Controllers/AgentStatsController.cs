using ClaimService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.API.Controllers
{
    [ApiController]
    [Route("api/agent/stats")]
    [Authorize(Roles = "Agent")]
    public class AgentStatsController : ControllerBase
    {
        private readonly ClaimServices _service;

        public AgentStatsController(ClaimServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgentStats()
        {
            var stats = await _service.GetAgentStats(HttpContext);
            return Ok(stats);
        }
    }
}
