using System.ComponentModel.DataAnnotations;

namespace PolicyService.API.DTOs
{
    public class CreatePolicyRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Premium is required")]
        public decimal Premium { get; set; }

        [Required(ErrorMessage = "Coverage is required")]
        public string Coverage { get; set; }

        [Required(ErrorMessage = "Terms are required")]
        public string Terms { get; set; }
    }
}