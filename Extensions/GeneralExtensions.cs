using System.Linq;
using Microsoft.AspNetCore.Http;

namespace blogapi.Extensions
{
    public static class GeneralExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null) return 0;

            return int.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }
    }
}