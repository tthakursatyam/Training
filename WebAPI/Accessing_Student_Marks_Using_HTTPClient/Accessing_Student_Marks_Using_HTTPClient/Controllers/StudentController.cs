using Microsoft.AspNetCore.Mvc;
using Accessing_Student_Marks_Using_HTTPClient.Model;
using Accessing_Student_Marks_Using_HTTPClient.Services;
namespace Accessing_Student_Marks_Using_HTTPClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly APIcalling _apiCalling;
        private readonly HttpClient _httpClient;
        public StudentController(APIcalling api,HttpClient client)
        {
            _apiCalling = api;
            _httpClient = client;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudentList()
        {
            var data = await _apiCalling.GetStudents();
            return Ok(data);
        }
    }
}
