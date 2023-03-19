using WebNotesApi.Models;
using WebNotesApi.Models.AutorizationModels;
using WebNotesApi.Services.Interface;

namespace WebNotesApi.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;
        public RefreshTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
        {
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(User user)
        {
            return _tokenGenerator.GenerateToken(_jwtSettings.RefreshTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience,
                _jwtSettings.RefreshTokenExpirationMinutes);
        }
    }
}
