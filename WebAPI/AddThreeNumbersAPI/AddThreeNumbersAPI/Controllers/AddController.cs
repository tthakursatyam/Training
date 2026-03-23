using AddThreeNumbersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddThreeNumbersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddNumbers(AddNumbersModel model)
        {
            int result = model.Number1 + model.Number2 + model.Number3;

            return Ok(new
            {
                sum = result
            });
        }
    }
}
