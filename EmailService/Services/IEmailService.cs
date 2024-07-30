using MimeKit;

namespace EmailService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MimeMessage message);
    }
}
