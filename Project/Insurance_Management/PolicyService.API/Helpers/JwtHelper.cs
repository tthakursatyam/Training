using System.Security.Claims;

namespace PolicyService.API.Helpers
{
    public static class JwtHelper
    {
        public static Guid GetUserId(HttpContext context)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.Parse(userId);
        }
        public static string GetEmail(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}