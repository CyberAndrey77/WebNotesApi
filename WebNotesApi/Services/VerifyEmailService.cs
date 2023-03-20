using Microsoft.EntityFrameworkCore;
using WebNotesApi.Models.AutorizationModels;
using WebNotesApi.Services.Interface;

namespace WebNotesApi.Services
{
    public class VerifyEmailService : IVerifyEmailService
    {
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;

        public VerifyEmailService(ApplicationContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<string> VerifyEmail(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.VerificationToken == token);
            if (user == null)
            {
                return "Неверный токен";
            }

            user.IsVerification = true;
            user.VerificationToken = null;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Почта подвержена успешно";
        }


        public async Task<AnswerModel> UpdateVerifyEmailToken(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new AnswerModel() { MessageError = "Пользователь не найден" };
            }

            string verificationToken = RandomToken.GetRandomToken();
            user.VerificationToken = verificationToken;
            await _context.SaveChangesAsync();
            await _emailService.SendVerificationOnEmailAsync(user.Email, user.VerificationToken);
            return new AnswerModel() { Message = "Ссылка отправлена вам на почту" };
        }

        public string GetVerifyToken()
        {
            return RandomToken.GetRandomToken();
        }
    }
}
