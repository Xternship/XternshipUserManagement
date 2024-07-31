using Grpc.Core;
using FluentEmail.Core;
using EmailService.Proto;
using Microsoft.Extensions.Logging;

namespace EmailService.Services
{
    public class EmailServiceImpl : EmailServiceProto.EmailServiceProtoBase, IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<EmailServiceImpl> _logger;

        public EmailServiceImpl(IFluentEmail fluentEmail, ILogger<EmailServiceImpl> logger)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
        }

        public override async Task<SendEmailResponse> SendEmail(SendEmailRequest request, ServerCallContext context)
        {
            return await SendEmailAsync(request, context);
        }

        public async Task<SendEmailResponse> SendEmailAsync(SendEmailRequest request, ServerCallContext context)
        {
            try
            {
                var response = await _fluentEmail
                    .To(request.Email)
                    .Subject("Welcome to Xternship")
                    .Body($"Hello {request.Username},\n\nYour account has been created. Your password is: {request.Password}")
                    .SendAsync();

                if (response.Successful)
                {
                    return new SendEmailResponse { Message = "Email sent successfully" };
                }
                else
                {
                    _logger.LogError("Failed to send email.");
                    return new SendEmailResponse { Message = "Failed to send email" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while sending email.");
                return new SendEmailResponse { Message = "Failed to send email" };
            }
        }
    }
}
