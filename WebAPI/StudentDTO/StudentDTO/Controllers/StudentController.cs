using Microsoft.AspNetCore.Mvc;
using StudentDTO.DTO;
using StudentDTO.Models;

namespace StudentDTO.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>();
        [HttpPost]
        public IActionResult CreateStudent(CreateRequestDTO reuqest)
        {
            Student st = new Student
            {
                Id = reuqest.Id,
                Name = reuqest.Name,
                Age = reuqest.Age
            };
            students.Add(st);
            return Ok(st);
        }
        [HttpPut("marks")]
        public IActionResult UpdateMarks([FromBody] UpdateRequestDTO request)
        {
            var student = students.FirstOrDefault(s => s.Id == request.Id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            student.M1 = request.M1;
            student.M2 = request.M2;
            student.Total = request.M1 + request.M2;

            return Ok(student);
        }

    }
}
