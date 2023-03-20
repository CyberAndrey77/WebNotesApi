using Microsoft.AspNetCore.Mvc;
using WebNotesApi.Models.AutorizationModels;
using WebNotesApi.Services.Interface;

namespace WebNotesApi.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost("reset_password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return Ok(await _passwordService.ResetPasswordAsync(resetPasswordModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot_password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                return Ok(await _passwordService.CreateVerificationPasswordToken(email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
