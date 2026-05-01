using System.Text;
using System.Text.Json;

namespace PolicyService.API.ExternalServices
{
    public class PaymentClient
    {
        private readonly HttpClient _httpClient;

        public PaymentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreatePayment(int policyId, decimal amount)
        {
            var request = new
            {
                policyId = policyId,
                amount = amount
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "http://localhost:5013/api/payment/create-order",
                content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Payment service failed");

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}