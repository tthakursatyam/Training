using Microsoft.AspNetCore.Mvc;
using WebAPIDTO.Model;
using WebAPIDTO.DTO;

namespace WebAPIDTO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>();

        // POST: api/student
        [HttpPost]
        public IActionResult CreateStudent(CreateRequestDTO request)
        {
            Student student = new Student
            {
                Id = students.Count + 1,
                Name = request.Name,
                Age = request.Age,
                CourseFee = request.CourseFee
            };

            students.Add(student);

            return Ok(student.Id);
        }

        // GET: api/student/basic
        [HttpGet("basic")]
        public ActionResult<List<ResponseDTO>> GetStudents()
        {
            var result = students.Select(s => new ResponseDTO
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return Ok(result);
        }

        // GET: api/student/totalfees
        [HttpGet("totalfees")]
        public ActionResult<decimal> GetTotalFees()
        {
            decimal total = students.Sum(s => s.CourseFee);
            return Ok(total);
        }
    }
}