using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Framework.Executor;
using FieldFactory.Framework.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldFactory.Api.Controllers.Map
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        LocationExecutor executor = new LocationExecutor();

        // GET: api/Location
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Location/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Location/5/explore
        [HttpPost("{idLocation}/explore", Name = "ExploreLocation")]
        public string ExploreLocation(int idLocation)
        {
            var query = new GetRandomEventForLocationQuery(idLocation);
            var eventStep = executor.Execute(query);

            // Get location from db
            // Select a random event for this location
            //Return the step 0 of event


            return "value";
        }


        // POST: api/Location
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Location/5
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
