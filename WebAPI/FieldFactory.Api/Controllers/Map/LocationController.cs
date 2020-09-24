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
        LocationExecutor executor
        {
            get
            {
                return new LocationExecutor(GetIdentity());
            }
        }

        // GET: api/Location/dummyLocation/
        [HttpGet("{idLocation}")]
        public string Get(string idLocation)
        {
            var query = new GetLocation(idLocation);
            var location = executor.Execute(query);

            return ConvertResponseToJson(location);
        }
        /// <summary>
        /// Renvoie une location et les actions en cours dessus
        /// </summary>
        /// <param name="idLocation"></param>
        /// <returns></returns>
        // GET: api/Location/dummyLocation/withactions
        [HttpGet("{idLocation}/withactions")]
        public string GetWithActions(string idLocation)
        {
            var query = new GetLocationWithActions(idLocation);
            var locationWithActions = executor.Execute(query);

            return locationWithActions;
        }


        // GET: api/Location/dummyLocation/verbs
        [HttpGet("{idLocation}/verbs", Name = "GetVerbs")]
        public IEnumerable<string> GetVerbs(string idLocation)
        {
            var query = new GetLocation(idLocation);
            var location = executor.Execute(query);

            var verbs = location.Verbs.Select(v => v.ToString());
            return verbs;
        }

        [Obsolete("Passer par ExploreController")]
        // POST: api/Location/5/explore
        [HttpPost("{idLocation}/explore", Name = "ExploreLocation")]
        public string ExploreLocation(int idLocation)
        {
            // Get location from db
            // Select a random event for this location
            //Return the step 0 of event

            var query = new GetRandomEventForLocationQuery(idLocation.ToString());
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