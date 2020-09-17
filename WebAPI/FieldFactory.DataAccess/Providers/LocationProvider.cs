using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;

namespace FieldFactory.DataAccess.Providers
{
    public class LocationProvider : SQLiteBaseProvider
    {
        public LocationDTO GetLocation(int idLocation)
        {
            var query = SQLiteLocationStringBuilder.SelectLocationByIdQuery(idLocation);
            var location = Read(query);
            return new LocationDTO();
        }
    }
}
