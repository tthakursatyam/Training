using ClaimService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.API.Controllers
{
    [ApiController]
    [Route("api/admin/stats")]
    [Authorize(Roles = "Admin")]
    public class AdminStatsController : ControllerBase
    {
        private readonly ClaimServices _service;

        public AdminStatsController(ClaimServices service)
        {
            _service = service;
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaimStats()
        {
            var stats = await _service.GetClaimStats();
            return Ok(stats);
        }
    }
}
