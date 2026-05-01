using System.ComponentModel.DataAnnotations;

namespace ClaimService.API.DTOs
{
    public class CreateQueryRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
