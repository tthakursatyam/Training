using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // Implement your code here
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }

        [HttpDelete("DeleteStudent/{studentId}")]
        public IActionResult DeleteStudent(int studentId)
        {
            var result = _student.DeleteStudent(studentId);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("ByCourseTitle/{courseTitle}")]
        public IActionResult ByCourseTitle(string courseTitle)
        {
            var students = _student.GetStudentsByCourseTitle(courseTitle);
            if (students != null && students.Any()) return Ok(students);
            return NotFound("No Records Found");
        }
    }
}
