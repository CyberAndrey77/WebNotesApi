using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebNotesApi.Models;

namespace WebNotesApi.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ApplicationContext _context;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenValidator _refreshTokenValidator;

        public AuthorizationService(ApplicationContext context, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService, IRefreshTokenValidator refreshTokenValidator)
        {
            _context = context;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
            _refreshTokenValidator = refreshTokenValidator;
        }

        public async Task<List<string>> Login(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                throw new ArgumentException("Неверная почта или пароль");
            }

            if (!VerifyPassword(model.Password, user.Password))
            {
                throw new ArgumentException("Неверная почта или пароль");
            }

            var refreshToken = _refreshTokenService.GenerateToken(user);
            await PutRefreshTokenInDB(user.Id, refreshToken);
            return new List<string>() { _accessTokenService.GenerateToken(user), refreshToken };
        }

        public async Task<List<string>> RefreshToken(RefreshTokenModel model)
        {
            if (!_refreshTokenValidator.Validate(model.RefreshToken))
            {
                throw new ArgumentException("Токен не верный");
            }

            var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == model.RefreshToken);

            if (token == null)
            {
                throw new ArgumentException("Токен не верный");
            }

            var user = await _context.Users.FindAsync(token.UserId);

            if (user == null)
            {
                throw new ArgumentException("Пользователь не найден");
            }

            _context.RefreshTokens.Remove(token);

            var refreshToken = _refreshTokenService.GenerateToken(user);
            await PutRefreshTokenInDB(user.Id, refreshToken);

            await _context.SaveChangesAsync();
            return new List<string>() { _accessTokenService.GenerateToken(user), refreshToken };
        }

        public async Task<string> Registration(RegistrationModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null)
            {
                return "Данная эл. почта уже используется";
            }

            user = new User()
            {
                Email = model.Email,
                Name = model.Name,
                Password = CreatePasswordHash(model.Password),
                CreationTime = DateTime.Now.ToString("dd.MM.yyyy"),
                Role = UserRole.Unsigned.ToString()
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return "Успешная регистрация";
        }

        private byte[] CreatePasswordHash(string password)
        {
            byte[] passwordHash;
            using (SHA512 shaM = new SHA512Managed())
            {
                passwordHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return passwordHash;
        }

        private bool VerifyPassword(string enteredPassword, byte[] password)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                var computedHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(enteredPassword));
                return computedHash.SequenceEqual(password);
            }
        }

        private async Task PutRefreshTokenInDB(int id, string refreshToken)
        {
            await _context.RefreshTokens.AddAsync(new RefreshToken() { UserId = id, Token = refreshToken });
            await _context.SaveChangesAsync();
        }
    }
}
