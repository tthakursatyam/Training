using System.ComponentModel.DataAnnotations;

namespace PolicyService.API.DTOs
{
    public class RenewPolicyRequest
    {
        [Required(ErrorMessage = "CustomerPolicyId is required")]
        public int CustomerPolicyId { get; set; }
    }
}