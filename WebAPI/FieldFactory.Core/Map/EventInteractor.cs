using FieldFactory.Core.Entities.Map;
using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;
using System;

namespace FieldFactory.Core.Map
{
    public class EventInteractor
    {
        LocationProvider locationProvider = new LocationProvider();
        EventProvider eventProvider = new EventProvider();

        public Tuple<string, Event> GetRandomEventForLocation(string idLocation)
        {
            var locationDto = locationProvider.Get(idLocation);
            var location = JsonConvert.DeserializeObject<Location>(locationDto.Json);
            var randomEventId = location.GetARandomEvent();

            var eventDTO = eventProvider.Get(randomEventId);
            var evt = JsonConvert.DeserializeObject<Event>(eventDTO.Json);

            return new Tuple<string, Event>(randomEventId, evt);
        }

    }
}
