namespace PaymentService.API.DTOs
{
    public class VerifyPaymentRequest
    {
        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }
        public string? UserId { get; set; }
        public int PolicyId { get; set; }
    }
}
