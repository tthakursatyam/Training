namespace PolicyService.API.DTOs
{
    public class ConfirmPaymentDto
    {
        public int PolicyId { get; set; }
        public int PaymentId { get; set; }
        public string Status { get; set; }
    }
}