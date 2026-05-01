using System.ComponentModel.DataAnnotations;

public class Payment
{
    [Key]
    public int Id { get; set; }

    public int PolicyId { get; set; }

    public decimal Amount { get; set; }

    public string RazorpayOrderId { get; set; }

    public string Status { get; set; } // Created, Success, Failed

    public string? UserId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}