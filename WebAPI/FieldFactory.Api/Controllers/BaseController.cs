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
    }
}
