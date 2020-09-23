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
                var idPlayer = CookieHelper.GetFromCookie(HttpContext, "ffidentity");
                _identity = new Identity() { IdPlayer = idPlayer };
            }

            return _identity;
        }

    }
}
