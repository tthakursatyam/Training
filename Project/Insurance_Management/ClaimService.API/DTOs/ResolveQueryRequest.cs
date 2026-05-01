using System.ComponentModel.DataAnnotations;

namespace ClaimService.API.DTOs
{
    public class ResolveQueryRequest
    {
        [Required(ErrorMessage = "Query Id is required")]
        public int QueryId { get; set; }

        [Required(ErrorMessage = "Response is required")]
        public string Response { get; set; }
    }
}
