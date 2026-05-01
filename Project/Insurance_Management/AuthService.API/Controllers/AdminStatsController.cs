using AuthService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/admin/stats")]
    [Authorize(Roles = "Admin")]
    public class AdminStatsController : ControllerBase
    {
        private readonly AuthServices _service;

        public AdminStatsController(AuthServices service)
        {
            _service = service;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUserStats()
        {
            var stats = await _service.GetUserStats();
            return Ok(stats);
        }
    }
}
