using global::PaymentService.API.Data;
using global::PaymentService.API.DTOs;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;
namespace PaymentService.API.Services
{

    public class PaymentServices
    {
        private readonly PaymentDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<PaymentServices> _logger;

        public PaymentServices(PaymentDbContext context, IConfiguration config, ILogger<PaymentServices> logger)
        {
            _context = context;
            _config = config;
            _logger = logger;
        }

        private RazorpayClient GetClient()
        {
            return new RazorpayClient(
                _config["Razorpay:Key"],
                _config["Razorpay:Secret"]);
        }

        public async Task<object> CreateOrder(CreatePaymentRequest dto)
        {
            var client = GetClient();

            var options = new Dictionary<string, object>
            {
                { "amount", dto.Amount * 100 },
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString() }
            };

            var order = client.Order.Create(options);

            var orderId = order["id"].ToString();
            var orderAmount = order["amount"].ToString();
            var orderCurrency = order["currency"].ToString();

            var payment = new Payment
            {
                PolicyId = dto.PolicyId,
                Amount = dto.Amount,
                RazorpayOrderId = orderId,
                Status = "Created",
                UserId = dto.UserId
            };

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            // Return a plain object so System.Text.Json serializes it correctly
            return new
            {
                id = orderId,
                amount = orderAmount,
                currency = orderCurrency
            };
        }

        public async Task VerifyPayment(VerifyPaymentRequest dto)
        {
            _logger.LogInformation("VerifyPayment called with OrderId={OrderId}, PaymentId={PaymentId}, Signature={Signature}, UserId={UserId}, PolicyId={PolicyId}",
                dto.RazorpayOrderId, dto.RazorpayPaymentId, dto.RazorpaySignature, dto.UserId, dto.PolicyId);

            if (string.IsNullOrEmpty(dto.RazorpayOrderId))
                throw new Exception("RazorpayOrderId is required.");
            if (string.IsNullOrEmpty(dto.RazorpayPaymentId))
                throw new Exception("RazorpayPaymentId is required.");
            if (string.IsNullOrEmpty(dto.RazorpaySignature))
                throw new Exception("RazorpaySignature is required.");

            // Bypassing Razorpay signature verification per user request.
            _logger.LogWarning("Bypassing Razorpay signature verification.");

            var payment = await _context.Payments
                .FirstOrDefaultAsync(x => x.RazorpayOrderId == dto.RazorpayOrderId);

            var policyId = dto.PolicyId;
            var userId = dto.UserId;

            if (payment != null)
            {
                payment.Status = "Success";
                policyId = payment.PolicyId;
                userId = dto.UserId ?? payment.UserId;
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Payment record not found for order: {OrderId}. Proceeding with policy activation using provided PolicyId={PolicyId}.", dto.RazorpayOrderId, dto.PolicyId);
            }

            // Call Policy Service to activate the purchased policy
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    if (!Guid.TryParse(userId, out var userGuid))
                    {
                        _logger.LogError("UserId '{UserId}' is not a valid GUID. Cannot call PolicyService.", userId);
                        throw new Exception($"Invalid UserId format: '{userId}'. Expected a valid GUID.");
                    }

                    using var http = new HttpClient();
                    var confirmPayload = new
                    {
                        PolicyId = policyId,
                        UserId = userGuid
                    };
                    _logger.LogInformation("Calling PolicyService confirm-payment with payload: PolicyId={PolicyId}, UserId={UserId}",
                        policyId, userId);

                    var response = await http.PostAsJsonAsync("http://localhost:5169/api/policy/confirm-payment", confirmPayload);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("PolicyService response: Status={Status}, Body={Body}", response.StatusCode, responseBody);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("PolicyService confirm-payment failed: Status={Status}, Body={Body}", response.StatusCode, responseBody);
                        throw new Exception($"Policy activation failed: {responseBody}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to call PolicyService confirm-payment");
                    throw; // Re-throw so the controller returns a proper error response
                }
            }
            else
            {
                _logger.LogError("UserId is null or empty after payment verification. Cannot activate policy.");
                throw new Exception("UserId is missing. Cannot activate policy.");
            }
        }
    }
}
