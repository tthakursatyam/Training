using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyService.API.Services;

namespace PolicyService.API.Controllers
{
    [ApiController]
    [Route("api/admin/stats")]
    [Authorize(Roles = "Admin")]
    public class AdminStatsController : ControllerBase
    {
        private readonly PolicyServices _service;

        public AdminStatsController(PolicyServices service)
        {
            _service = service;
        }

        [HttpGet("policies")]
        public async Task<IActionResult> GetPolicyStats()
        {
            var stats = await _service.GetPolicyStats();
            return Ok(stats);
        }
    }
}
