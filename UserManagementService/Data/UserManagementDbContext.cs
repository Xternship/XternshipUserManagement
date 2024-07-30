using Microsoft.EntityFrameworkCore;
using UserManagementService.Data.Entities;

namespace UserManagementService.Data
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
