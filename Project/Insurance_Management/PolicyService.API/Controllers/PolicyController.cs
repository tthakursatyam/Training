using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyService.API.DTOs;
using PolicyService.API.Services;
using PolicyService.API.Data;
using Microsoft.EntityFrameworkCore;

namespace PolicyService.API.Controllers
{
    [ApiController]
    [Route("api/policy")]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyServices _service;
        private readonly PolicyDbContext _context;

        public PolicyController(PolicyServices service, PolicyDbContext context)
        {
            _service = service;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePolicyRequest dto)
        {
            await _service.CreatePolicy(dto);
            return Ok("Policy Created");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, CreatePolicyRequest dto)
        {
            await _service.UpdatePolicy(id, dto);
            return Ok(new { message = "Policy updated successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeletePolicy(id);
            return Ok(new { message = "Policy deleted successfully" });
        }

        [HttpGet("all-policies")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllPolicies();
            return Ok(result);
        }

        [HttpPost("purchase")]
        [Authorize]
        public async Task<IActionResult> Purchase(PurchasePolicyRequest dto)
        {
            await _service.PurchasePolicy(dto, HttpContext);
            return Ok("Policy Purchased Successfully");
        }

        [HttpGet("my-policies")]
        [Authorize]
        public async Task<IActionResult> GetMyPolicies()
        {
            var result = await _service.GetMyPolicies(HttpContext);
            return Ok(result);
        }
        [HttpGet("download/{customerPolicyId}")]
        public async Task<IActionResult> DownloadPolicy(int customerPolicyId)
        {
            var fileBytes = await _service.GeneratePolicyPdf(customerPolicyId);

            return File(fileBytes, "application/pdf", $"Policy_{customerPolicyId}.pdf");
        }

        [HttpPost("renew")]
        [Authorize]
        public async Task<IActionResult> Renew(RenewPolicyRequest dto)
        {
            await _service.RenewPolicy(dto, HttpContext);
            return Ok(new { message = "Policy renewed successfully" });
        }

        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(ConfirmPaymentRequest dto)
        {
            await _service.ConfirmPayment(dto);
            return Ok("Payment confirmed and policy activated");
        }
    }
}