using Microsoft.AspNetCore.Mvc;
using TwoFactorAuth.Domain.Models;
using TwoFactorAuth.Services.Abstract;

namespace TwoFactorAuth.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITFAService _tFAService;

        public AuthController(ITFAService tFAService)
        {
            this._tFAService = tFAService;
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] VerificationCodeRequest request)
        {
            var success = await _tFAService.SendCode(request).ConfigureAwait(false);
            return success ? Ok(new { Message = "Code sent successfully" }) : BadRequest(new { Message = "Failed to send code" });
        }
        [HttpPost("check-code")]
        public async Task<IActionResult> CheckConfirmationCode([FromBody] VerifyConfirmationCodeRequest request)
        {
            var success = await _tFAService.VerifyCode(request).ConfigureAwait(false);
            return success ? Ok(new { Message = "Code validated successfully" }) : BadRequest(new { Message = "Invalid code" });
        }
    }
}
