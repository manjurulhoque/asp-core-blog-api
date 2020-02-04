using System.Linq;
using Microsoft.AspNetCore.Http;

namespace blogapi.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetCurrentUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null) return string.Empty;

            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}