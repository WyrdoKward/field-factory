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

        /// <summary>
        /// Returns current player if a session is active (cookie ffauthtoken)
        /// </summary>
        /// <returns></returns>
        // GET: api/Auth/session
        [HttpGet("session", Name = "GetSession")]
        public Player GetSession()
        {
            var identity = GetIdentity();

            return identity.Player;
        }

        // POST: api/Auth/login
        //{"IdPlayer":"wyrdokward","Mdp":"123456"}
        [HttpPost("login")]
        public Player Post([FromBody] LoginQuery query)
        {
            var player = executor.Execute(query);
            if (string.IsNullOrEmpty(player.Token))
                throw new Exception("Pas loggué");

            SetIdentityCookie(player.Token);

            player.SanitizePlayer();
            //return "Hello "+executor.Identity.Player.IdPlayer;
            return player;
        }

    }
}
