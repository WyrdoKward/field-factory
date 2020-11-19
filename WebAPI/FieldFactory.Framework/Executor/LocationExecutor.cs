using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Enum;
using FieldFactory.Core.Map;
using FieldFactory.Core.Verbs;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;
using System;

namespace FieldFactory.Framework.Executor
{
    public class LocationExecutor : BaseExecutor
    {
        public LocationExecutor(Identity identity)
        {
            Identity = identity;
        }


        public Location Execute(GetLocation query)
        {
            LocationInteractor locationInteractor = new LocationInteractor();
            var location = locationInteractor.GetLocation(query.LocationId);

            return location;
        }

        public LocationWithActions Execute(GetLocationWithActions query)
        {
            LocationWithActions locationWithActions = new LocationWithActions();

            LocationInteractor locationInteractor = new LocationInteractor();
            locationWithActions.Location = locationInteractor.GetLocation(query.LocationId);


            foreach (var v in locationWithActions.Location.Verbs)
            {
                switch (v)
                {
                    case EVerb.Explorer:
                        ExploreInteractor exploreInteractor = new ExploreInteractor();
                        try
                        {
                            locationWithActions.Explore = exploreInteractor.GetExplorationForLocation(Identity.Player.IdPlayer, query.LocationId);
                        }
                        catch (Exception e)
                        {
                            locationWithActions.Explore = null;
                        }
                        break;
                    default:
                        break;
                }
            }

            return locationWithActions;

        }
    }
}
