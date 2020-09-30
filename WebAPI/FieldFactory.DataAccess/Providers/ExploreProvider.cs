using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.Providers
{
    public class ExploreProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 7; //Voir pour gérer nb de col directement dans la requete
        public void Add(ExploreDTO exploration)
        {
            //Ajouter unicité sur IdPlayer/idlocation
            var query = SQLiteExploreQueryBuilder.AddExploreQuery(exploration);
            ExecuteSingleNonQuery(query);
        }

        public ExploreDTO Get(string idPlayer, string idLocation)
        {
            var query = SQLiteExploreQueryBuilder.GetExploreQuery(idPlayer, idLocation);
            var rawExploreDto = ReadColumns(query, NB_COL_IN_TABLE);
            return ConvertIntoDto(rawExploreDto)[0]; //On devrait en recevoir qu'un seul
        }

        public void Update(ExploreDTO explore)
        {
            var query = SQLiteExploreQueryBuilder.UpdateExploreQuery(explore);
            ExecuteSingleNonQuery(query);
        }

        private List<ExploreDTO> ConvertIntoDto(Dictionary<int, List<string>> rawLocations)
        {
            var res = new List<ExploreDTO>();

            foreach (var rawLocation in rawLocations)
            {
                res.Add(new ExploreDTO(rawLocation.Value[0], rawLocation.Value[1],rawLocation.Value[2], rawLocation.Value[3], int.Parse(rawLocation.Value[4]), DateTime.Parse(rawLocation.Value[5]), rawLocation.Value[6]));
            }

            return res;
        }

    }
}
