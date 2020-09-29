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

        /// <summary>
        /// Sérialize l'objet en paramètre, en gardant la casse des membres
        /// </summary>
        internal string ConvertResponseToJson<T>(T objectToSerialize)
        {
            var json = JsonConvert.SerializeObject(objectToSerialize, new Newtonsoft.Json.Converters.StringEnumConverter(),);

            return json;
        }

        private Identity _identity;

        /// <summary>
        /// Récupère l'identité du joueur dans le cookie d'authent
        /// </summary>
        internal Identity GetIdentity()
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
        
        internal void SetIdentityCookie(string token)
        {
             CookieHelper.SetCookie(HttpContext, AUTH_COOKIE, token);
        }

    }
}
