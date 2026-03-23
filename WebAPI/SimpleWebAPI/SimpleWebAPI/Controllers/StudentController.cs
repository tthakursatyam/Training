using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Models;
namespace SimpleWebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudent()
        {
            var students = new List<Student>
            {
                new Student {Id = 1, Name="Satyam",Marks=90},
                new Student {Id = 1, Name="Sandeep",Marks=80},
                new Student {Id = 1, Name="Raman",Marks=70},
            };
            return Ok(students);
        }

        [HttpGet("add")]
        public IActionResult AddThreeNumber(int a, int b, int c)
        {
            int res = a + b + c;
            return Ok(res);
        }
        [HttpGet("AddHundred")]
        public IActionResult AddNumbers()
        {
            int res = 0;
            for(int i=1;i<=100;i++)
            {
                res = i + res;
            }
            return Ok(res);
        }

    }
}
