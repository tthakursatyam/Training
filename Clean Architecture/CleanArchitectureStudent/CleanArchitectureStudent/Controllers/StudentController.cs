using Microsoft.AspNetCore.Mvc;
using StudentCleanArch.Application.Interfaces;
using StudentCleanArch.Domain.Entities;

namespace StudentCleanArch.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo;

        public StudentController(IStudentRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            var result = await _repo.AddStudent(student);
            return Ok(result);
        }

        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAllStudents();
            return Ok(data);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _repo.GetStudentById(id);
            return Ok(data);
        }
    }
}
