namespace ClaimService.API.Helpers
{
    using System.Security.Claims;

    public static class JwtHelper
    {
        public static Guid GetUserId(HttpContext context)
        {
            var id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(id);
        }

        public static string GetRole(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
