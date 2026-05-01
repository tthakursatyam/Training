using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.API.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Otp { get; set; }
        public DateTime? OtpExpiry { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; }
    }
}
