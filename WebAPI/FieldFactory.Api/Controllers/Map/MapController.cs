using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FieldFactory.Api.Controllers.Map
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        // GET: api/Map
        [HttpGet]
        public string Get()
        {
            var res = MapGenerator.GenerateMap(MapGenerator.Template1);
            string json = JsonConvert.SerializeObject(res, new Newtonsoft.Json.Converters.StringEnumConverter());
            return json;
        }

        // GET: api/Map/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Map
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Map/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
