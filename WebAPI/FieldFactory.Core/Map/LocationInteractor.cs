using FieldFactory.Core.Entities.Map;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FieldFactory.Core.Map
{
    public class LocationInteractor
    {
        LocationProvider locationProvider = new LocationProvider();

        public IEnumerable<string> GetVerbsForLocation(int locationId)
        {
            var locationDto = locationProvider.Get(locationId);
            var location = JsonConvert.DeserializeObject<Location>(locationDto.Json);

            var verbs = location.Verbs.Select(v => v.ToString());

            return verbs;
        }
    }
}
