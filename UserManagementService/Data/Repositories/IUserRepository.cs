using UserManagementService.Data.Entities;
using System.Threading.Tasks;

namespace UserManagementService.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(int id); 
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user); 
        Task SaveChangesAsync();
    }
}
