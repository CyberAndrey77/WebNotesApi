using Microsoft.EntityFrameworkCore;
using WebNotesApi.Models.AutorizationModels;
using WebNotesApi.Services.Interface;
using System.Security.Cryptography;

namespace WebNotesApi.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;

        public PasswordService(ApplicationContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public byte[] CreatePasswordHash(string password)
        {
            byte[] passwordHash;
            using (SHA512 shaM = new SHA512Managed())
            {
                passwordHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return passwordHash;
        }

        public async Task<AnswerModel> CreateVerificationPasswordToken(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new AnswerModel() { MessageError = "Пользователь не найден" };
            }
            if (!user.IsVerification)
            {
                return new AnswerModel() { MessageError = "Почта не подверждена" };
            }

            user.PasswordVerificationToken = RandomToken.GetRandomToken();
            user.DateExpirationPasswordVerificationToken = DateTime.Now.AddDays(1).ToString();
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await _emailService.SendLinkForResetPasswordOnEmailAsync(user.Email, user.PasswordVerificationToken);
            return new AnswerModel() { Message = "Пройдите по ссылке присланой на электронную почту" };
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            if (resetPasswordModel.Password != resetPasswordModel.RepetedPassword)
            {
                return "Пароли не совпадают";
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.PasswordVerificationToken == resetPasswordModel.Token);
            if (user == null)
            {
                return "Неверный токен";
            }

            DateTime enteredDate = DateTime.Parse(user.DateExpirationPasswordVerificationToken);

            if (DateTime.Now > enteredDate)
            {
                return "Время для смены пароля истекло, попробуйте еще раз";
            }

            user.Password = CreatePasswordHash(resetPasswordModel.Password);
            user.DateExpirationPasswordVerificationToken = null;
            user.PasswordVerificationToken = null;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Пароль обновлен успешно";
        }

        public bool VerifyPassword(string enteredPassword, byte[] password)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                var computedHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(enteredPassword));
                return computedHash.SequenceEqual(password);
            }
        }
    }
}
