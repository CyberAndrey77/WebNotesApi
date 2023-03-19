namespace WebNotesApi.Models.AutorizationModels
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
