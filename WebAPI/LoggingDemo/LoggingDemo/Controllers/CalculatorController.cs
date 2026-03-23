using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    [ApiController]
    [Route("api/calculator")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            _logger.LogInformation("Add method called with {A} and {B}", a, b);

            int result = a + b;

            _logger.LogInformation("Addition result: {Result}", result);

            return Ok(result);
        }

        [HttpGet("divide")]
        public IActionResult Divide(int a, int b)
        {
            _logger.LogInformation("Divide method called with {A} and {B}", a, b);

            if (b == 0)
            {
                _logger.LogWarning("Division attempted with zero");
                return BadRequest("Cannot divide by zero");
            }

            int result = a / b;

            _logger.LogInformation("Division result: {Result}", result);

            return Ok(result);
        }
    }
}
