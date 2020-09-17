using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FieldFactory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/Test
        [HttpGet]
        public string Get()
        {
            CnxTester cnx = new CnxTester();
            cnx.CreateTable();
            cnx.InsertBeers();
            var beers = cnx.GetBeers();
            string json = JsonConvert.SerializeObject(beers, new Newtonsoft.Json.Converters.StringEnumConverter());
            return json;
        }

        
    }
}
