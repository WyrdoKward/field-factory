using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.Core.Map;
using FieldFactory.Framework.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Executor
{
    public class LocationExecutor
    {
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
    }
}
