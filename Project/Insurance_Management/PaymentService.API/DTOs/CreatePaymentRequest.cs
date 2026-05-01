namespace PaymentService.API.DTOs
{
    public class CreatePaymentRequest
    {
        public int PolicyId { get; set; }
        public decimal Amount { get; set; }
        public string? UserId { get; set; }
    }
}
