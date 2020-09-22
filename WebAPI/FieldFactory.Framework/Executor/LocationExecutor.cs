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


            EventGetter eventGetter = new EventGetter();
            var step0 = eventGetter.GetRandomEventForLocation(query.LocationId);

            return step0;
        }

        public IEnumerable<string> Execute(GetVerbsForLocation query)
        {
            LocationInteractor locationInteractor = new LocationInteractor();
            var verbs = locationInteractor.GetVerbsForLocation(query.LocationId);

            return verbs;
        }
    }
}
