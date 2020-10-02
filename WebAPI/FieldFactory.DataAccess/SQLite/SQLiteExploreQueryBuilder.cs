using FieldFactory.DataAccess.DTO;
using System;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteExploreQueryBuilder
    {
        internal static string AddExploreQuery(ExploreDTO dto)
        {
            return $"INSERT INTO Explore (idPlayer, idFollower, idLocation, idEvent, idStep, dateNextStep, stepsHistory) VALUES " +
                $"('{dto.IdPlayer}', '{dto.IdFollower}', '{dto.IdLocation}', '{dto.IdEvent}', '{dto.IdStep}', '{dto.DateNextStep}', '{dto.StepHistory}')";
        }

        internal static string GetExploreQuery(string idPlayer, string idLocation)
        {
            return $"SELECT * FROM Explore Where idPlayer = '{idPlayer}' and idLocation = '{idLocation}'";
        }

        internal static string UpdateExploreQuery(ExploreDTO dto)
        {
            return $"UPDATE Explore SET idStep = {dto.IdStep}, dateNextStep = '{dto.DateNextStep}', stepsHistory = '{dto.StepHistory}' WHERE idPlayer = '{dto.IdPlayer}' and idLocation = '{dto.IdLocation}'";
        }


        internal static string DeleteExploreQuery(string idPlayer, string idLocation)
        {
            return $"DELETE FROM Explore Where idPlayer = '{idPlayer}' and idLocation = '{idLocation}'";
        }
    }
}
