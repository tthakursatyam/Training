using Microsoft.AspNetCore.Mvc;
using Student_Hostel.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Student_Hostel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent(CreateStudentDTO dto)
        {
            await _service.CreateStudent(dto);
            return Ok("Student Created");
        }

        [HttpPut("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDTO dto)
        {
            await _service.UpdateRoomNo(dto);
            return Ok();
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _service.DeleteStudent(id);
            return Ok();
        }

        [HttpGet("ReadAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _service.GetAllStudents());
        }

        [HttpGet("ReadAllStudentsInHostel")]
        public async Task<IActionResult> GetStudentsInHostel()
        {
            return Ok(await _service.GetStudentsInHostel());
        }
    }
}
