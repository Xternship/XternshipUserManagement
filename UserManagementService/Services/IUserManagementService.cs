using System.Threading.Tasks;
using UserManagementService.Dtos;
using UserManagementService.Protos;

namespace UserManagementService.Services
{
    public interface IUserManagementService
    {
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
    }
}
