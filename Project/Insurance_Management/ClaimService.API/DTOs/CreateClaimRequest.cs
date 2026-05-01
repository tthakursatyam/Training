using System.ComponentModel.DataAnnotations;

public class CreateClaimRequest
{
    [Required]
    public int PolicyId { get; set; }

    public string Description { get; set; }
}