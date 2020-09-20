﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.Framework.Executor;
using FieldFactory.Framework.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FieldFactory.Api.Controllers.Map
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : BaseController
    {
        LocationExecutor executor = new LocationExecutor();

        // GET: api/Location
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Location/5/explore
        [HttpPost("{idLocation}/explore", Name = "ExploreLocation")]
        public string ExploreLocation(int idLocation)
        {
            // Get location from db
            // Select a random event for this location
            //Return the step 0 of event

            // TODO ajouter l'enregistrer de la position courrante du player
            var query = new GetRandomEventForLocationQuery(idLocation);
            var eventStep = executor.Execute(query);

            return ConvertResponseToJson(eventStep);
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
