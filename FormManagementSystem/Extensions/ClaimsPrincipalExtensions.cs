using System.Security.Claims;

namespace FormManagementSystem.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this System.Security.Claims.ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
