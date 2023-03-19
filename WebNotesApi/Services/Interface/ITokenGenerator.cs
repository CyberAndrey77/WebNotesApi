using System.Security.Claims;

namespace WebNotesApi.Services.Interface
{
    /// <summary>
    /// Интервейс для генерации токена.
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Генерация jwt токена.
        /// </summary>
        /// <returns></returns>
        string GenerateToken(string secretKey, string issuer, string audience, double expires, IEnumerable<Claim> claims);
        /// <summary>
        /// Генерация jwt токена.
        /// </summary>
        /// <returns></returns>
        string GenerateToken(string secretKey, string issuer, string audience, double expires);
    }
}
