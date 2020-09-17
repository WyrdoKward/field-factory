using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Map
{
    public class EventGetter
    {
        LocationProvider locationProvider = new LocationProvider();
        EventProvider eventProvider = new EventProvider();

        public EventStep GetRandomEventForLocation(int idLocation)
        {
            var locationDto = locationProvider.Get(idLocation);
            var location = JsonConvert.DeserializeObject<Location>(locationDto.Json);
            var randomEvent = location.GetARandomEvent();


            var eventDTO = eventProvider.Get(randomEvent);
            var evt = JsonConvert.DeserializeObject<Event>(eventDTO.Json);
            return evt.Steps[0];
        }
    }
}
