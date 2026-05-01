using System.ComponentModel.DataAnnotations;

namespace PolicyService.API.DTOs
{
    public class PurchasePolicyRequest
    {
        [Required(ErrorMessage = "PolicyId is required")]
        public int PolicyId { get; set; }
    }
}