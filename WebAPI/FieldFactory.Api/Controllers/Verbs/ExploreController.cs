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
        public string Post([FromBody] AddExplorationWithFollower vm)
        {
            var query = new AddExplorationWithFollowerQuery(executor.Identity.Player.IdPlayer, vm.IdFollower, vm.IdLocation);
            var step0 = executor.Execute(query);

            return ConvertResponseToJson(step0); //déjà renvoyer un array avec juste 1 step ?

            //Renvoyer un 200 via IActionResult ?
        }

        // PUT: api/Explore/
        // {"IdLocation": "dummyLocation", "IdChoice": 2}
        [HttpPut]
        public string Put([FromBody] RegisterEventChoice vm)
        {
            var query = new RegisterEventChoiceQuery(executor.Identity.Player.IdPlayer, vm.IdLocation, vm.IdChoice);
            var explore = executor.Execute(query);

            return ConvertResponseToJson(explore);
        }
    }
}
