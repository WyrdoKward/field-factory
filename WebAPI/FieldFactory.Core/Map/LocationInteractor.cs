using FieldFactory.Core.Entities.Map;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;

namespace FieldFactory.Core.Map
{
    public class LocationInteractor
    {
        LocationProvider locationProvider = new LocationProvider();

        public Location GetLocation(string locationId)
        {
            var locationDto = locationProvider.Get(locationId);
            var location = JsonConvert.DeserializeObject<Location>(locationDto.Json);

            return location;
        }

        /// <summary>
        /// Calcule le temps de trajet entre 2 points sur la map
        /// </summary>
        /// <returns>TravelTime, in seconds</returns>
        public int ProcessTravelTime()
        {
            return 60;
        }
    }
}
