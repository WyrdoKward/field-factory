using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.Core.Entities.Verbs;
using FieldFactory.Core.Verbs;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;
using System;

namespace FieldFactory.Framework.Executor
{
    public class ExploreExecutor : BaseExecutor
    {
        ExploreInteractor exploreInteractor = new ExploreInteractor();

        public ExploreExecutor(Identity identity)
        {
            Identity = identity;
        }

        public Explore Execute(AddExplorationWithFollowerQuery query)
        {
            Explore exploration = new Explore()
            {
                IdPlayer = query.IdPlayer,
                IdFollower = query.IdFollower,
                IdLocation = query.IdLocation
            };
            return exploreInteractor.AddNewExploration(exploration);
        }

        public Explore Execute(RegisterEventChoiceQuery query)
        {
            Explore exploration = new Explore()
            {
                IdPlayer = query.IdPlayer,
                IdLocation = query.IdLocation,
                IdChoice = query.IdChoice
            };

            return exploreInteractor.RegisterEventChoiceOnLocation(exploration);            
        }

        public Explore Execute(GetExploreForLocationQuery query)
        {
            return exploreInteractor.GetExplorationForLocation(query.IdPlayer, query.IdLocation);
        }
    }
}
