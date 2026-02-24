using Microsoft.AspNetCore.Mvc;
using MVC001.Models;
namespace MVC001.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Student_Data(int? id ,string name,int? m1,int? m2,int? m3)
        {
            StudentData student = new StudentData()
            {
                Id = id,
                Name = name,
                M1= m1,
                M2 = m2,
                M3 = m3
            };
            return View(student);
        }
        
    }
}
