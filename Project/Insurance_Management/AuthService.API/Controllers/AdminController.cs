using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthService.API.Services;
using AuthService.API.DTOs;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AuthServices _service;

        public AdminController(AuthServices service)
        {
            _service = service;
        }

        [HttpPost("create-agent")]
        public async Task<IActionResult> CreateAgent(CreateAgentRequest dto)
        {
            await _service.CreateAgent(dto);
            return Ok("Agent Created");
        }
        [HttpGet("agents")]
        public async Task<IActionResult> GetAgents()
        {
            var result = await _service.GetAllAgents();
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _service.GetAllUsers();
            return Ok(result);
        }


        [HttpPost("create-claim-adjuster")]
        public async Task<IActionResult> CreateClaimAdjuster(CreateClaimAdjusterRequest dto)
        {
            await _service.CreateClaimAdjuster(dto);
            return Ok("Claim Adjuster Created");
        }
        [HttpGet("claim-adjusters")]
        public async Task<IActionResult> GetClaimAdjusters()
        {
            var result = await _service.GetAllClaimAdjusters();
            return Ok(result);
        }

        [HttpPut("toggle-user/{id}")]
        public async Task<IActionResult> Toggle(Guid id)
        {
            await _service.ToggleUserStatus(id);
            return Ok("User status updated");
        }
    }
}