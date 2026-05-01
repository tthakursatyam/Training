using ClaimService.API.DTOs;
using ClaimService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.API.Controllers
{
    [ApiController]
    [Route("api/query")]
    public class QueryController : ControllerBase
    {
        private readonly ClaimServices _service;

        public QueryController(ClaimServices service)
        {
            _service = service;
        }

        [HttpPost("submit")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SubmitQuery(CreateQueryRequest dto)
        {
            await _service.CreateQuery(dto, HttpContext);
            return Ok("Query submitted successfully.");
        }

        [HttpGet("my-queries")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyQueries()
        {
            var result = await _service.GetMyQueries(HttpContext);
            return Ok(result);
        }

        [HttpPost("reopen")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ReopenQuery(ReopenQueryRequest dto)
        {
            await _service.ReopenQuery(dto, HttpContext);
            return Ok("Query reopened successfully.");
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> GetPendingQueries()
        {
            var result = await _service.GetPendingQueries();
            return Ok(result);
        }

        [HttpGet("my-assigned-query")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> GetMyAssignedQuery()
        {
            var result = await _service.GetMyAssignedQuery(HttpContext);
            return Ok(result); // Returns null if no query is currently assigned
        }

        [HttpPost("resolve")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> ResolveQuery(ResolveQueryRequest dto)
        {
            await _service.ResolveQuery(dto, HttpContext);
            return Ok("Query resolved successfully.");
        }

        [HttpPost("upload")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Upload(int queryId, IFormFile file)
        {
            await _service.UploadQueryDocument(queryId, file);
            return Ok("Document uploaded successfully.");
        }

        [HttpGet("document/{queryId}")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> GetDocument(int queryId)
        {
            var doc = await _service.GetQueryDocument(queryId);
            if (doc == null) return NotFound("Document not found");

            return File(doc.Content, "application/octet-stream", doc.FileName);
        }

        [HttpGet("all-queries")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllQueries()
        {
            var result = await _service.GetAllQueries();
            return Ok(result);
        }
    }
}
