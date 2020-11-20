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

        public string GetRandomEventForLocation(string idLocation)
        {
            var locationDto = locationProvider.Get(idLocation);
            var location = JsonConvert.DeserializeObject<Location>(locationDto.Json);
            var randomEventId = location.GetARandomEvent();

            return randomEventId;
        }

    }
}
