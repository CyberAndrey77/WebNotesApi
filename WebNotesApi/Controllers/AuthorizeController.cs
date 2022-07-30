using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebNotesApi.Models;
using WebNotesApi.Services;

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
        public async Task<ActionResult<DataTokens>> Login(LoginModel model)
        {
            try
            {
                List<string> tokens = await _authorization.Login(model);
                //первым элементом идет токен доступа, втрым в списке идт токен перевыска
                var dataTokens = new DataTokens() { AccessToken = tokens[0], RefreshToken = tokens[1] };
                return Ok(dataTokens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("regitration")]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            try
            {
                await _authorization.Registration(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<DataTokens>> RefreshToken(RefreshTokenModel model)
        {
            try
            {
                List<string> tokens = await _authorization.RefreshToken(model);
                //первым элементом идет токен доступа, втрым в списке идт токен перевыска
                var dataTokens = new DataTokens() { AccessToken = tokens[0], RefreshToken = tokens[1] };
                return Ok(dataTokens);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
