using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;

namespace FieldFactory.DataAccess.Providers
{
    public class LocationProvider : SQLiteBaseProvider
    {
        public LocationDTO Get(int idLocation)
        {
            var query = SQLiteLocationStringBuilder.SelectLocationByIdQuery(idLocation);
            var location = Read(query);
            return new LocationDTO(); // convertir résultat de sqlite en dto
        }
    }
}
