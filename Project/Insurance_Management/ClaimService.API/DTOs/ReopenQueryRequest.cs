using System.ComponentModel.DataAnnotations;

namespace ClaimService.API.DTOs
{
    public class ReopenQueryRequest
    {
        [Required(ErrorMessage = "Query Id is required")]
        public int QueryId { get; set; }

        [Required(ErrorMessage = "Reason or follow-up question is required")]
        public string AdditionalComment { get; set; }
    }
}
