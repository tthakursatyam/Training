using Microsoft.AspNetCore.Mvc;

namespace APIDTODemo.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
