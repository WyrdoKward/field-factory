using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldFactory.Api.ViewModel;
using FieldFactory.Core.Entities.Map.Event;
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
        public EventStep Post([FromBody] AddExplorationWithFollower vm)
        {
            var query = new AddExplorationWithFollowerQuery(executor.Identity.Player.IdPlayer, vm.IdFollower, vm.IdLocation);
            var step0 = executor.Execute(query);

            return step0;

            //Renvoyer un 200 via IActionResult ?
        }
    }
}
