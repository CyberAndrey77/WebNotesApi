using System.Security.Cryptography;

namespace WebNotesApi.Services
{
    public static class RandomToken
    {
        public static string GetRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
