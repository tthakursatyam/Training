
using Microsoft.AspNetCore.Mvc;
using MVC_With_ADO.NET.Data;

namespace MVC_With_ADO.NET.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _repo;

        public StudentController(StudentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult GetStudentData()
        {
            var students = _repo.GetAllStudents();
            return View(students);
        }
    }
}
