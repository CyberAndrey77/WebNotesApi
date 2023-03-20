using Microsoft.AspNetCore.Mvc;
using WebNotesApi.Services.Interface;

namespace WebNotesApi.Controllers
{
    public class EmailController : Controller
    {
        private readonly IVerifyEmailService _verifyService;

        public EmailController(IVerifyEmailService verifyService) 
        { 
            _verifyService = verifyService;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            try
            {
                return Ok(await _verifyService.VerifyEmail(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verification_email")]
        public async Task<IActionResult> VerificationEmail(string email)
        {
            try
            {
                return Ok(await _verifyService.UpdateVerifyEmailToken(email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
