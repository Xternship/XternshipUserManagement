using EmailService;  
using Grpc.Core;
using MimeKit;
using EmailService.Proto;
using MailKit.Net.Smtp;

namespace EmailService.Services
{
    public class EmailServiceImpl : EmailServiceProto.EmailServiceProtoBase, IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailServiceImpl()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Connect("smtp.yourserver.com", 587, false);
            _smtpClient.Authenticate("yourusername", "yourpassword");
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            await _smtpClient.SendAsync(message);
            await _smtpClient.DisconnectAsync(true);
        }

        public override async Task<SendEmailResponse> SendEmail(SendEmailRequest request, ServerCallContext context)
        {
            var emailMessage = new MimeMessage
            {
                Subject = request.Subject,
                Body = new TextPart("plain") { Text = request.Body }
            };

            emailMessage.From.Add(new MailboxAddress("YourApp", "noreply@yourapp.com"));
            emailMessage.To.Add(new MailboxAddress(request.ToName, request.ToEmail));

            try
            {
                await SendEmailAsync(emailMessage);
                return new SendEmailResponse { Success = true };
            }
            catch
            {
                return new SendEmailResponse { Success = false };
            }
        }
    }
}