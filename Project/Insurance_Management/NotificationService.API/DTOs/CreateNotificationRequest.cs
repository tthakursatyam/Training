namespace NotificationService.API.DTOs
{
    public class CreateNotificationRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
