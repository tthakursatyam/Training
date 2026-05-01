using System.ComponentModel.DataAnnotations;

namespace ClaimService.API.Entities
{
    public class Query
    {
        [Key]
        public int Id { get; set; }
        
        public Guid CustomerId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public string? Response { get; set; }
        
        // e.g., Pending, Resolved, Reopened
        public string Status { get; set; } = "Pending"; 
        
        public Guid? AgentId { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
