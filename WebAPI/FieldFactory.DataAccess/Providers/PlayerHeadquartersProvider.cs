using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;

namespace FieldFactory.DataAccess.Providers
{
    public class PlayerHeadquartersProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 4;		

        public PlayerHeadquartersDTO Get(string idPlayer)
        {
            var query = SQLitePlayerHeadquartersQueryBuilder.GetPlayerHeadquartersQuery(idPlayer);
            var rawPlayerHeadquartersDto = ReadColumns(query, NB_COL_IN_TABLE);
            if (rawPlayerHeadquartersDto.Count == 0)
                throw new Exception("No results");

            return ConvertIntoDto(rawPlayerHeadquartersDto)[0]; //There should be only one ?
        }
		
        public void Add(PlayerHeadquartersDTO playerHeadquarters)
        {
            var query = SQLitePlayerHeadquartersQueryBuilder.AddPlayerHeadquartersQuery(playerHeadquarters);
            ExecuteSingleNonQuery(query);
        }

        public void Update(PlayerHeadquartersDTO explore)
        {
            var query = SQLitePlayerHeadquartersQueryBuilder.UpdatePlayerHeadquartersQuery(explore);
            ExecuteSingleNonQuery(query);
        }

        public void Delete(string idPlayer)
        {
            var query = SQLitePlayerHeadquartersQueryBuilder.DeletePlayerHeadquartersQuery(idPlayer);
            ExecuteSingleNonQuery(query);
        }

        private List<PlayerHeadquartersDTO> ConvertIntoDto(Dictionary<int, Dictionary<string, string>> PlayerHeadquarterss)
        {
            var res = new List<PlayerHeadquartersDTO>();

            foreach (var rawPlayerHeadquarters in PlayerHeadquarterss)
            {
                res.Add(new PlayerHeadquartersDTO(rawPlayerHeadquarters.Value["idPlayer"], rawPlayerHeadquarters.Value["idHeadquarters"], int.Parse(rawPlayerHeadquarters.Value["dummyFurniture"]), int.Parse(rawPlayerHeadquarters.Value["watchtower"])));
            }

            return res;
        }

    }
}
