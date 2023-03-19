using WebNotesApi.Models.AutorizationModels;

namespace WebNotesApi.Services.Interface
{
    public interface IPasswordService
    {
        byte[] CreatePasswordHash(string password);
        bool VerifyPassword(string enteredPassword, byte[] password);
        Task<string> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    }
}
