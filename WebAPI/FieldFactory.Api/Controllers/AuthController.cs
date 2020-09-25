using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Core.Entities;
using FieldFactory.Framework.Executor;
using FieldFactory.Framework.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldFactory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        AuthExecutor _executor;
        AuthExecutor executor
        {
            get
            {
                if(_executor == null)
                    _executor = new AuthExecutor(null);

                return _executor;
            }
        }  

        // POST: api/Auth/login
       /* [HttpPost("login")]
        public void Post([FromBody] string idPlayer, string pwd)
        {
            var query = new LoginQuery(idPlayer, pwd);
            var token = executor.Execute(query);
        }*/
        
        ///POST /api/Person/UnProtected HTTP/1.1
        ///Host: localhost:5000
        ///Accept: application/json, text/javascript, */*; q=0.01
        ///Content-Type: application/json; charset=UTF-8
        ///
        ///{"IdPlayer":"wyrdokward","Mdp":"123456"}
        
        // POST: api/Auth/login
        [HttpPost("login")]
        public string Post([FromBody] LoginQuery query)
        {
            var player = executor.Execute(query);
            if (string.IsNullOrEmpty(player.Token))
                throw new Exception("Pas loggué");

            SetIdentityCookie(player.Token);
            return "Hello "+executor.Identity.Player.IdPlayer;
            //return "Hello "+player.IdPlayer;
        }

    }
}
