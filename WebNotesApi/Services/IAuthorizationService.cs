using WebNotesApi.Models;

namespace WebNotesApi.Services
{
    public interface IAuthorizationService
    {
        Task<string> Registration(RegistrationModel model);

        Task<List<string>> Login(LoginModel model);

        Task<List<string>> RefreshToken(RefreshTokenModel model);
    }
}
