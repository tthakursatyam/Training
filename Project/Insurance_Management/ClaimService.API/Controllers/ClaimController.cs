using ClaimService.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClaimService.API.Services;
[ApiController]
[Route("api/claim")]
public class ClaimController : ControllerBase
{
    private readonly ClaimServices _service;

    public ClaimController(ClaimServices service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Create(CreateClaimRequest dto)
    {
        var claimId = await _service.CreateClaim(dto, HttpContext);
        return Ok(new { claimId = claimId });
    }

    [HttpPost("upload")]
    [Authorize(Roles = "Customer")]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB limit
    public async Task<IActionResult> Upload([FromQuery] int claimId, IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "No file provided." });

        await _service.UploadDocument(claimId, file);
        return Ok(new { message = "Uploaded" });
    }

    [HttpGet("my-claims")]
    [Authorize(Roles = "ClaimAdjuster")]
    public async Task<IActionResult> GetAssigned()
    {
        var result = await _service.GetAssignedClaims(HttpContext);
        return Ok(result);
    }

    [HttpPost("action")]
    [Authorize(Roles = "ClaimAdjuster")]
    public async Task<IActionResult> Action(ApproveRejectDto dto)
    {
        await _service.ApproveReject(dto, HttpContext);
        return Ok("Updated");
    }

    [HttpGet("customer-claims")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetCustomerClaims()
    {
        var result = await _service.GetCustomerClaims(HttpContext);
        return Ok(result);
    }

    [HttpGet("all-claims")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllClaims()
    {
        var result = await _service.GetAllClaims();
        return Ok(result);
    }

    [HttpGet("adjuster-stats")]
    [Authorize(Roles = "ClaimAdjuster")]
    public async Task<IActionResult> GetAdjusterStats()
    {
        var result = await _service.GetAdjusterStats(HttpContext);
        return Ok(result);
    }

    [HttpGet("document/{claimId}")]
    [Authorize(Roles = "ClaimAdjuster")]
    public async Task<IActionResult> GetClaimDocument(int claimId)
    {
        var doc = await _service.GetClaimDocument(claimId);
        if (doc == null)
            return NotFound(new { message = "No document found for this claim." });

        return File(doc.Content, doc.ContentType, doc.FileName);
    }
}