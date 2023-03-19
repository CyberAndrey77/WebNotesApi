namespace WebNotesApi.Services.Interface
{
    public interface IVerifyService
    {
        Task<string> VerifyEmail(string token);
    }
}
