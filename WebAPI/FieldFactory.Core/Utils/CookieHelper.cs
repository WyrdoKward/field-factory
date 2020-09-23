using Microsoft.AspNetCore.Http;

namespace FieldFactory.Core.Utils
{
    public static class CookieHelper
    {
        public static string GetFromCookie(HttpContext httpContext, string cookieName)
        {
            string cookie = httpContext.Request.Cookies[cookieName];
            return cookie;
        }
    }
}
