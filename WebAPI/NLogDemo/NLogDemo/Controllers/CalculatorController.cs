using Microsoft.AspNetCore.Mvc;

namespace NLogDemo.Controllers
{
    [ApiController]
    [Route("api/calc")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        // ADD API
        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            _logger.LogInformation("Add method started with values {A} and {B}", a, b);

            int result = a + b;

            _logger.LogInformation("Addition result is {Result}", result);

            return Ok(result);
        }

        // DIVIDE API
        [HttpGet("divide")]
        public IActionResult Divide(int a, int b)
        {
            _logger.LogInformation("Divide method started with values {A} and {B}", a, b);

            if (b == 0)
            {
                _logger.LogWarning("User tried division by zero");

                return BadRequest("Cannot divide by zero");
            }

            int result = a / b;

            _logger.LogInformation("Division result is {Result}", result);

            return Ok(result);
        }
    }
}