using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        AuthExecutor executor
        {
            get
            {
                return new AuthExecutor(GetIdentity());
            }
        }  

        // POST: api/Auth/login
        [HttpPost("login")]
        public void Post([FromBody] string idPlayer, string pwd)
        {
            var query = new LoginQuery(idPlayer, pwd);
            var token = executor.Execute(query);
        }

    }
}
