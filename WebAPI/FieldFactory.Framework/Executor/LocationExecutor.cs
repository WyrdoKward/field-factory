using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.Core.Enum;
using FieldFactory.Core.Map;
using FieldFactory.Core.Verbs;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FieldFactory.Framework.Executor
{
    public class LocationExecutor : BaseExecutor
    {
        public LocationExecutor(Identity identity)
        {
            Identity = identity;
        }

        public EventStep Execute(GetRandomEventForLocationQuery query)
        {
            //Validation ?
            //Droit d'accéder à cette lcoation ?


            EventInteractor eventGetter = new EventInteractor();
            var step0 = eventGetter.GetRandomEventForLocation(query.LocationId);

            return step0.Item2.Steps[0];
        }

        public Location Execute(GetLocation query)
        {
            LocationInteractor locationInteractor = new LocationInteractor();
            var location = locationInteractor.GetLocation(query.LocationId);

            return location;
        }

        public string Execute(GetLocationWithActions query)
        {
            LocationInteractor locationInteractor = new LocationInteractor();
            var location = locationInteractor.GetLocation(query.LocationId);

            Dictionary<EVerb, string> currentActions = new Dictionary<EVerb, string>();
                

            foreach (var v in location.Verbs)
            {
                var json = "";

            
                switch (v)
                {
                    case EVerb.Explorer:
                        ExploreInteractor exploreInteractor = new ExploreInteractor();
                        var explore = exploreInteractor.GetExplorationForLocation(Identity.IdPlayer, query.LocationId);
                        break;
                    default:
                        break;
                }


                currentActions.Add(v, json);
            }

            //On concatène la location avec les actions et on sérialize en json
            string res = "";



            return res;
            
        }
    }
}
