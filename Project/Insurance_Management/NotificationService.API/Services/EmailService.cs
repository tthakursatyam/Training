using MailKit.Net.Smtp;
using MimeKit;
using NotificationService.API.DTOs;

namespace NotificationService.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(SendEmailRequest request)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _config["Email:Name"],
                _config["Email:From"]
            ));

            message.To.Add(new MailboxAddress("", request.ToEmail));
            message.Subject = request.Subject;

            message.Body = new TextPart("plain")
            {
                Text = request.Body
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _config["Email:Smtp"],
                int.Parse(_config["Email:Port"]),
                false
            );

            await client.AuthenticateAsync(
                _config["Email:From"],
                _config["Email:Password"]
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}