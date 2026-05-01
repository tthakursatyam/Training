namespace AuthService.API.DTOs
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}