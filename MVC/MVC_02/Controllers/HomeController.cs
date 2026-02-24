using Microsoft.AspNetCore.Mvc;
using MVC_02.Models;
using System.Diagnostics;

namespace MVC_02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TestError()
        {
            int x = 10;
            int y = 0;
            int res = x / y;
            return Content(res.ToString());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SendAdd()
        {
            TempData["Message"] = "Hello";
            return RedirectToAction("Add", "TempData");
        }
        public IActionResult SendSquare(int? a)
        {
            TempData["x"] = a;
            return RedirectToAction("Square", "TempData");
        }

        public IActionResult SendInfo(string? name,string? college)
        {
            ViewData["Name"] = name;
            ViewData["College"] = college;
            return View();
        }
    }
}
