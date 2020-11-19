using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System.Collections.Generic;

namespace FieldFactory.DataAccess.Providers
{
    public class LocationProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 2;
        public LocationDTO Get(string idLocation)
        {
            var query = SQLiteLocationQueryBuilder.SelectLocationByIdQuery(idLocation);
            var location = ConvertIntoDto(ReadColumns(query, NB_COL_IN_TABLE));
            return location[0]; 
        }

        private List<LocationDTO> ConvertIntoDto(Dictionary<int, Dictionary<string, string>> rawLocations)
        {
            var res = new List<LocationDTO>();

            foreach (var rawLocation in rawLocations)
            {
                res.Add(new LocationDTO() { Id = rawLocation.Value["idLocation"], Json = rawLocation.Value["json"] });
            }
                
            return res;
        }
    }
}
