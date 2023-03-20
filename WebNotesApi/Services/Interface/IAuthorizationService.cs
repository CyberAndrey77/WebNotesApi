using WebNotesApi.Models.AutorizationModels;

namespace WebNotesApi.Services.Interface
{
    public interface IAuthorizationService
    {
        Task<AnswerModel> Registration(RegistrationModel model);

        Task<List<string>> Login(LoginModel model);

        Task<List<string>> RefreshToken(RefreshTokenModel model);
    }
}
