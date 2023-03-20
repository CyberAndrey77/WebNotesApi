using WebNotesApi.Models;
using Microsoft.AspNetCore.Mvc;
using WebNotesApi.Services.Interface;
using WebNotesApi.Models.AutorizationModels;

namespace WebNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizationService _authorization;
        public AuthorizeController(IAuthorizationService authorization)
        {
            _authorization = authorization;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                List<string> tokens = await _authorization.Login(model);
                //первым элементом идет токен доступа, втрым в списке идт токен перевыска
                var dataTokens = new DataTokens() { AccessToken = tokens[0], RefreshToken = tokens[1], ErrorMessage = string.Empty };
                return Ok(dataTokens);
            }
            catch (Exception ex)
            {
                return BadRequest(new DataTokens() { AccessToken = string.Empty, RefreshToken = string.Empty, ErrorMessage = ex.Message });
            }
        }

        [HttpPost("regitration")]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            try
            {
                return Ok(await _authorization.Registration(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            try
            {
                List<string> tokens = await _authorization.RefreshToken(model);
                //первым элементом идет токен доступа, втрым в списке идт токен перевыска
                var dataTokens = new DataTokens() { AccessToken = tokens[0], RefreshToken = tokens[1], ErrorMessage = string.Empty };
                return Ok(dataTokens);
            }
            catch (Exception ex)
            {

                return BadRequest(new DataTokens() { AccessToken = string.Empty, RefreshToken = string.Empty, ErrorMessage = ex.Message });
            }
        }
    }
}
