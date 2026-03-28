using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        // Implement your code here

        private readonly IInstructor _instructor;

        public InstructorController(IInstructor instructor)
        {
            _instructor = instructor;
        }
        [HttpPost("AddInstructor")]
        public IActionResult AddInstructor([FromBody] Instructor instructor)
        {
            var result = _instructor.AddInstructor(instructor);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("WithCourseCountAbove/{count}")]
        public IActionResult WithCourseCountAbove(int count)
        {
            var intructor = _instructor.GetInstructorsWithCourseCountAbove(count);
            if (intructor != null && intructor.Any())
                return Ok(intructor);
            return NotFound("No Records Found");
        }
        [HttpGet("WithMostEnrollments")]
        public IActionResult WithMosEnrollements()
        {
            var intructor = _instructor.GetInstructorsWithMostEnrollments();
            if (intructor != null && intructor.Any())
                return Ok(intructor);
            return NotFound("No Records Found");
        }
    }
}
