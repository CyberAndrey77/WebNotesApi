using Microsoft.EntityFrameworkCore;
using WebNotesApi.Services.Interface;

namespace WebNotesApi.Services
{
    public class VerifyService : IVerifyService
    {
        private readonly ApplicationContext _context;
        public VerifyService(ApplicationContext context)
        {
            _context = context;
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
    }
}
