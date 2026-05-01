namespace ClaimService.API.DTOs
{
    public class ApproveRejectDto
    {
        public int ClaimId { get; set; }
        public string Status { get; set; } // Approved / Rejected
    }
}
