using Grpc.Core;
using EmailService.Proto;
using System.Threading.Tasks;

namespace EmailService.Services
{
    public interface IEmailService
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailRequest request, ServerCallContext context);
    }
}
