using Microsoft.AspNetCore.Mvc;
using Repo_Pattern_With_TwoDB.Service;

namespace Repo_Pattern_With_TwoDB.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _service;

        public FileController(IFileService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await _service.UploadAsync(file);
            return Ok("Uploaded Successfully");
        }
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var result = await _service.DownloadAsync(id);

            return File(result.Content, "application/pdf", result.FileName);
        }
    }
}
