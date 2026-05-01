namespace ClaimService.API.DTOs
{
    public class QueryResponseDto
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Response { get; set; }
        public string Status { get; set; }
        public Guid? AgentId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
