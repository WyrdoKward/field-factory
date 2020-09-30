using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;

namespace FieldFactory.DataAccess.Providers
{
    public class PlayerProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 4;
        public PlayerDTO GetById(string idPlayer)
        {
            var query = SQLitePlayerQueryBuilder.SelectPlayerByIdQuery(idPlayer);
            var player = ConvertIntoDto(ReadColumns(query, NB_COL_IN_TABLE));
            return player[0]; 
        }

        public PlayerDTO GetByToken(string token)
        {
            var query = SQLitePlayerQueryBuilder.SelectPlayerByTokenQuery(token);
            var player = ConvertIntoDto(ReadColumns(query, NB_COL_IN_TABLE));
            return player[0];
        }

        public PlayerDTO GetWithPassword(string idPlayer, string hashPwd)
        {
            var query = SQLitePlayerQueryBuilder.SelectPlayerWithPwdQuery(idPlayer, hashPwd);
            var player = ConvertIntoDto(ReadColumns(query, NB_COL_IN_TABLE));
            return player[0];
        }

        public void SetToken(string idPlayer, string token)
        {
            var query = SQLitePlayerQueryBuilder.SetTokenQuery(idPlayer, token);
            ExecuteSingleNonQuery(query);
        }


        private List<PlayerDTO> ConvertIntoDto(Dictionary<int, List<string>> rawPlayers)
        {
            var res = new List<PlayerDTO>();

            foreach (var rawPlayer in rawPlayers)
            {
                res.Add(new PlayerDTO() { IdPlayer = rawPlayer.Value[0], Email = rawPlayer.Value[1], Hashpwd = rawPlayer.Value[2], Token = rawPlayer.Value[3] });
            }
                
            return res;
        }
    }
}

