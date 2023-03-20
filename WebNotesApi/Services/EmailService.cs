using MailKit.Net.Smtp;
using MimeKit;
using WebNotesApi.Services.Interface;

namespace WebNotesApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationContext _context;
        public EmailService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<string> SendLinkForResetPasswordOnEmailAsync(string email, string token)
        {
            var url = $"https://localhost:7288/api/Password/reset_password?token={token}";
            var text = $"<span>Для сброса пароля перейдите по <a href=\"{url}\">ссылке</a></span>";
            await SendMail(email, "Сброс пароля", text);
            return "Сообщение оптравлено на почту";
        }

        public async Task<string> SendVerificationOnEmailAsync(string email, string token)
        {
            var url = $"https://localhost:7288/api/Email/verify?token={token}";
            var text = $"<span>Для подверждения регистрации перейдите по <a href=\"{url}\">ссылке</a></span>";
            await SendMail(email, "Подверждение почты", text);
            return "Сообщение оптравлено на почту";
        }

        private async Task<bool> SendMail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("fine.andrey231@yandex.ru"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.yandex.ru", 465, true);
                await smtp.AuthenticateAsync("fine.andrey231@yandex.ru", "aefxgiqdngxcyfpz");
                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);
            }
            return true;
        }
    }
}
