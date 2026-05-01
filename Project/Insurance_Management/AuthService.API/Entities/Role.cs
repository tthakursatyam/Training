using System.ComponentModel.DataAnnotations;

namespace AuthService.API.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}