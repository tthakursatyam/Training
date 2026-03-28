using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using UniversityApi.Interfaces;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        // Implement your code here

        private readonly ICourse _course;

        public CourseController(ICourse course)
        {
            _course = course;
        }


        [HttpPut("UpdateCourse")]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            var res = _course.UpdateCourse(course);
            if (res) return Ok();
            return BadRequest();
        }

        [HttpGet("WithEnrollmentsAboveGrade/{grade}")]
        public IActionResult WithEnrollmentsAboveGrade(int grade)
        {
            var courses = _course.GetCoursesWithEnrollmentsAboveGrade(grade);
            if (courses is not null && courses.Any()) return Ok(courses);
            return NotFound("No Records Found");
        }
        [HttpGet("ByInstructorName/{instructorName}")]
        public IActionResult ByInstructorName(string instructorName)
        {
            var courses = _course.GetCoursesByInstructorName(instructorName);
            if (courses is not null && courses.Any()) return Ok(courses);
            return NotFound("No Records Found");
        }
    }
}
