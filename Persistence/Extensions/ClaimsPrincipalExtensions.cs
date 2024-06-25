using System.Security.Claims;

namespace ToGoodToGo.Persistence.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal model)
        {
            return model.FindFirstValue(ClaimTypes.NameIdentifier) as string;
        }
    }
}