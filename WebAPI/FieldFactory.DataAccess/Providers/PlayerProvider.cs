using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System.Collections.Generic;

namespace FieldFactory.DataAccess.Providers
{
    public class PlayerProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 4;
        public PlayerDTO Get(string idPlayer)
        {
            var query = SQLitePlayerStringBuilder.SelectPlayerByIdQuery(idPlayer);
            var player = ConvertIntoDto(ReadColumns(query, NB_COL_IN_TABLE));
            return player; 
        }


        private List<PlayerDTO> ConvertIntoDto(Dictionary<int, List<string>> rawPlayers)
        {
            var res = new List<PlayerDTO>();

            foreach (var rawPlayer in rawPlayers)
            {
                res.Add(new PlayerDTO() { IdPlayer = rawPlayer.Value[0], Email = rawPlayer.Value[1], HashPwd = rawPlayer.Value[2], Token = rawPlayer.Value[3] });
            }
                
            return res;
        }
    }
}

