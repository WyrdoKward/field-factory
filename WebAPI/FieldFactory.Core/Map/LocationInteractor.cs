using FieldFactory.Core.Entities.Map;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;

namespace FieldFactory.Core.Map
{
    public class LocationInteractor
    {
        LocationProvider locationProvider = new LocationProvider();

        public Location GetLocation(int locationId)
        {
            var locationDto = locationProvider.Get(locationId);
            var location = JsonConvert.DeserializeObject<Location>(locationDto.Json);

            return location;
        }
    }
}
