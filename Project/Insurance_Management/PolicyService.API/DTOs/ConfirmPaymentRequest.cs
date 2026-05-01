using System.ComponentModel.DataAnnotations;

namespace PolicyService.API.DTOs
{
    public class ConfirmPaymentRequest
    {
        [Required]
        public int PolicyId { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
    }
}