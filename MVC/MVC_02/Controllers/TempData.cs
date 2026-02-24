using Microsoft.AspNetCore.Mvc;

namespace MVC_02.Controllers
{
    public class TempData : Controller
    {
        public IActionResult Add()
        {
            var res = TempData["Message"];
            return Content(res?.ToString());
        }
        public IActionResult Square()
         {
            if (TempData["x"] != null)
            {
                int n = Convert.ToInt32(TempData["x"]);
                int res = n * n;

                return Content(res.ToString());
            }

            return Content("No data found");
        }
    }
}
