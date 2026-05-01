using Microsoft.AspNetCore.Mvc;
using PaymentService.API.DTOs;
using PaymentService.API.Services;
namespace PaymentService.API.Controllers
{

    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentServices _service;

        public PaymentController(PaymentServices service)
        {
            _service = service;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreatePaymentRequest dto)
        {
            try
            {
                var result = await _service.CreateOrder(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(VerifyPaymentRequest dto)
        {
            try
            {
                await _service.VerifyPayment(dto);
                return Ok(new { message = "Payment Verified" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
