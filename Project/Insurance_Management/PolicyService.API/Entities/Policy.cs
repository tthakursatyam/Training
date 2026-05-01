using System.ComponentModel.DataAnnotations;
namespace PolicyService.API.Entities
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Policy name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Policy type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Premium is required")]
        public decimal Premium { get; set; }

        [Required(ErrorMessage = "Coverage is required")]
        public string Coverage { get; set; }

        [Required(ErrorMessage = "Terms are required")]
        public string Terms { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? Status { get; set; }
    }
}