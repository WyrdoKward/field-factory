using FieldFactory.DataAccess.DTO;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLitePlayerHeadquartersQueryBuilder
    {
        internal static string GetPlayerHeadquartersQuery(string idPlayer)
        {
            return $"SELECT * FROM PlayerHeadquarters Where idPlayer = '{idPlayer}'";
        }
		
        //TODO : utiliser tous les champs
        internal static string AddPlayerHeadquartersQuery(PlayerHeadquartersDTO dto)
        {
            return $"INSERT INTO PlayerHeadquarters (idPlayer, idHeadquarters, dummyFurniture, watchtower) VALUES " +
                $"('{dto.IdPlayer}', '{dto.IdHeadquarters}', '{dto.DummyFurniture}', '{dto.Watchtower}')";
        }

        //TODO : utiliser tous les champs
        internal static string UpdatePlayerHeadquartersQuery(PlayerHeadquartersDTO dto)
        {
            return $"UPDATE PlayerHeadquarters SET idPlayer = '{dto.IdPlayer}', idHeadquarters = '{dto.IdHeadquarters}', dummyFurniture = '{dto.DummyFurniture}', watchtower = '{dto.Watchtower}' WHERE idPlayer = '{dto.IdPlayer}'";
        }


        internal static string DeletePlayerHeadquartersQuery(string idPlayer)
        {
            return $"DELETE FROM PlayerHeadquarters Where idPlayer = '{idPlayer}'";
        }
    }
}
