using System.Threading.Tasks;

namespace EmailService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string toName, string subject, string body);
    }
}
