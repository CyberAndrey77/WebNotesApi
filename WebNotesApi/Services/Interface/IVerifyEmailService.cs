using WebNotesApi.Models.AutorizationModels;

namespace WebNotesApi.Services.Interface
{
    public interface IVerifyEmailService
    {
        Task<string> VerifyEmail(string token);
        Task<AnswerModel> UpdateVerifyEmailToken(string email);
        string GetVerifyToken();
    }
}
