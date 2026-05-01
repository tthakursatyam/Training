using AuthService.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/internal")]
    public class InternalController : ControllerBase
    {
        private readonly AuthServices _service;

        public InternalController(AuthServices service)
        {
            _service = service;
        }

        [HttpGet("agents")]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _service.GetAllAgents();
            return Ok(agents.Select(a => a.Id).ToList());
        }

        [HttpGet("claim-adjusters")]
        public async Task<IActionResult> GetClaimAdjusters()
        {
            var adjusters = await _service.GetAllClaimAdjusters();
            return Ok(adjusters.Select(a => a.Id).ToList());
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _service.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _service.GetAdmins();
            return Ok(admins);
        }
    }
}
