using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC001.Models;

namespace MVC001.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Student()
        {
            var s = new { Name = "Satyam Singh", Id = 12, mjbjbdfsdjbfmb = 89 };
            return Json(s);
        }
        public IActionResult Square(int? number)
        {
            if (number == null)
            {
                return Content("Number is required.");
            }
            else
            return View(number.Value);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
