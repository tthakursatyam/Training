using System.ComponentModel.DataAnnotations;

namespace ClaimService.API.Entities
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public int PolicyId { get; set; }

        public string Description { get; set; }

        public string Status { get; set; } // Pending, Assigned, Approved, Rejected

        public Guid? AssignedAdjusterId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}