using System.Text;
using System.Text.Json;

namespace PolicyService.API.ExternalServices
{
    public class NotificationClient
    {
        private readonly HttpClient _http;

        public NotificationClient(HttpClient http)
        {
            _http = http;
        }

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            var request = new
            {
                toEmail = toEmail,
                subject = subject,
                body = body
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            await _http.PostAsync(
                "http://localhost:5286/api/notification/send-email",
                content
            );
        }
    }
}