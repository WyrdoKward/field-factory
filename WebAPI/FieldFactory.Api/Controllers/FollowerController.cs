using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Framework.Executor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldFactory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : BaseController
    {
        FollowerExecutor executor
        {
            get
            {
                return new FollowerExecutor(GetIdentity());
            }
        }

        // GET: api/Follower
        // Get all followers of player
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Follower/Gustav
        [HttpGet("{idFollower}", Name = "GetFollower")]
        public string Get(string idFollower)
        {
            //On appelle directement l'exécuteur. Si y'a + d'infos que l'id, on passera par un query
            var follower = executor.Execute(idFollower);
            return ConvertResponseToJson(follower);
        }

    }
}
