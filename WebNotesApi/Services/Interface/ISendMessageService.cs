namespace WebNotesApi.Services.Interface
{
    public interface ISendMessageService
    {
        Task<string> SendMessage(string message);
    }
}
