using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;

namespace FieldFactory.DataAccess.Providers
{
    public class ExploreProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 8; //Voir pour gérer nb de col directement dans la requete
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
            if (rawExploreDto.Count == 0)
                throw new Exception("Ce joueur n'a pas d'explo à cet endroit");

            return ConvertIntoDto(rawExploreDto)[0]; //On devrait en recevoir qu'un seul
        }

        public void Update(ExploreDTO explore)
        {
            var query = SQLiteExploreQueryBuilder.UpdateExploreQuery(explore);
            ExecuteSingleNonQuery(query);
        }

        public void Delete(string idPlayer, string idLocation)
        {
            var query = SQLiteExploreQueryBuilder.DeleteExploreQuery(idPlayer, idLocation);
            ExecuteSingleNonQuery(query);
        }

        private List<ExploreDTO> ConvertIntoDto(Dictionary<int, Dictionary<string, string>> rawLocations)
        {
            var res = new List<ExploreDTO>();

            foreach (var rawLocation in rawLocations)
            {
                int? idChoice;
                if (string.IsNullOrEmpty(rawLocation.Value["idChoice"]))
                    idChoice = null;
                else
                    idChoice = int.Parse(rawLocation.Value["idChoice"]);

                res.Add(new ExploreDTO(rawLocation.Value["idPlayer"], rawLocation.Value["idFollower"], rawLocation.Value["idLocation"], rawLocation.Value["idEvent"], int.Parse(rawLocation.Value["idStep"]), idChoice, DateTime.Parse(rawLocation.Value["dateNextStep"]), rawLocation.Value["stepsHistory"]));
            }

            return res;
        }

    }
}
