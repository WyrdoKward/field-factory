using Microsoft.AspNetCore.Http;

namespace FieldFactory.Core.Utils
{
    public static class CookieHelper
    {
        private const int AUTH_COKKIE_EXPIRE_TIME_H = 12;
        
        public static string GetFromCookie(HttpContext httpContext, string cookieName)
        {
            string cookie = httpContext.Request.Cookies[cookieName];
            return cookie;
        }
        
        
        public static void SetCookie(HttpContext httpContext, string cookieName, string cookieValue)
        {
           CookieOptions options = new CookieOptions();  
           options.Expires = DateTime.Now.AddHours(AUTH_COKKIE_EXPIRE_TIME_H);  
           options.HttpOnly = true;

           Response.Cookies.Append(cookieName, cookieValue, options); 
        }
    }
    
}
