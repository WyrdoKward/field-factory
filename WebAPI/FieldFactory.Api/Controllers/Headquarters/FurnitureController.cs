using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Framework.Executor;
using FieldFactory.Framework.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldFactory.Api.Controllers.Headquarters
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : BaseController
    {
        FurnitureExecutor executor
        {
            get
            {
                return new FurnitureExecutor(GetIdentity());
            }
        }
        // GET: api/Furniture
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Furniture/possessed
        [HttpGet("possessed", Name = "GetPossessedFurniture")]
        public string GetPossessedFurniture()
        {
            var query = new GetPossessedFurnituresQuery(executor.Identity.Player.IdPlayer);
            var furnitures = executor.Execute(query);

            return ConvertResponseToJson(furnitures);
        }

        // POST: api/Furniture
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Furniture/5
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
