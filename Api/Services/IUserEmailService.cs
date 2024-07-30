using ApiGateway.Dtos;

namespace ApiGateway.Services
{
    public interface IUserEmailService
    {
        Task<ApiResponse> RegisterUserAndSendEmailAsync(RegisterUserDto request);
        Task<ApiResponse> RegisterUsersAndSendEmailsAsync(IEnumerable<RegisterUserDto> requests);
    }
}
