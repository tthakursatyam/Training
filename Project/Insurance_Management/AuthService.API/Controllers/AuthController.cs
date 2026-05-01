using AuthService.API.DTOs;
using AuthService.API.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _service;

        public AuthController(AuthServices service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest dto)
        {
            var result = await _service.Login(dto);
            return Ok(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            await _service.RevokeToken(refreshToken);
            return Ok("Logged out successfully");
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _service.RefreshToken(refreshToken);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest dto)
        {
            await _service.Register(dto);
            return Ok(new { message = "Registration started. Please verify your OTP." });
        }

        [HttpPost("verify-registration")]
        public async Task<IActionResult> VerifyRegistration(VerifyOtpRequest dto)
        {
            await _service.VerifyRegistrationOtp(dto.Email, dto.Otp);
            return Ok(new { message = "Registration verified successfully." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest dto)
        {
            await _service.ForgotPassword(dto.Email);
            return Ok(new { message = "If the email is registered, an OTP has been sent." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest dto)
        {
            await _service.ResetPassword(dto.Email, dto.Otp, dto.NewPassword);
            return Ok(new { message = "Password has been reset successfully." });
        }
    }
}