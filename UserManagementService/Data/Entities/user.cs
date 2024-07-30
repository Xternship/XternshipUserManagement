using System.ComponentModel.DataAnnotations;

namespace UserManagementService.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PasswordHash { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
