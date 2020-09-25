using FieldFactory.Core.Secutity;
using FieldFactory.Core.Utils;
using FieldFactory.Framework.Authorizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldFactory.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private const string AUTH_COOKIE = "ffauthtoken";

        internal string ConvertResponseToJson<T>(T objectToSerialize)
        {
            var json = JsonConvert.SerializeObject(objectToSerialize, new Newtonsoft.Json.Converters.StringEnumConverter());

            return json;
        }

        private Identity _identity;
        public Identity GetIdentity()
        {
            if (_identity == null)
            {
                var token = CookieHelper.GetFromCookie(HttpContext, AUTH_COOKIE);
                if (!string.IsNullOrEmpty(token))
                {
                    AuthInteractor authInteractor = new AuthInteractor();
                    var player = authInteractor.GetPlayerFromToken(token);
                    _identity = new Identity(player);
                }
                else
                    throw new Exception("Pas loggué"); //403
            }

            return _identity;
        }
        
        public void SetIdentityCookie(string token)
        {
             CookieHelper.SetCookie(HttpContext, AUTH_COOKIE, token);
        }

    }
}
