using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Api.ViewModel;
using FieldFactory.Framework.Executor;
using FieldFactory.Framework.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldFactory.Api.Controllers.Verbs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExploreController : BaseController
    {
        ExploreExecutor executor
        {
            get
            {
                return new ExploreExecutor(GetIdentity());
            }
        }

        // POST: api/Explore
        [HttpPost]
        public void Post([FromBody] AddExplorationWithFollower vm)
        {
            //Apeller ici GetRandomEventForLocation ?
            var query = new AddExplorationWithFollowerQuery(executor.Identity.Player.IdPlayer, vm.IdFollower, vm.IdLocation, vm.IdEvent, 0, DateTime.Now.AddSeconds(10));
            executor.Execute(query);

            //Renvoyer un 200 via IActionResult ?
        }
    }
}
