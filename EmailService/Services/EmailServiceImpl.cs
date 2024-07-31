using FluentEmail.Core;
using System.Threading.Tasks;

namespace EmailService.Services
{
    public class EmailServiceImpl : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailServiceImpl(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string toEmail, string toName, string subject, string body)
        {
            var response = await _fluentEmail
                .To(toEmail)
                .Subject(subject)
                .Body(body)
                .SendAsync();

            if (!response.Successful)
            {
                var errorMessages = string.Join(", ", response.ErrorMessages);
                throw new Exception($"FluentEmail request failed: {errorMessages}");
            }
        }
    }
}
