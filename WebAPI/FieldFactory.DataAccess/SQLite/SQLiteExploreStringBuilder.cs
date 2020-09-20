using System;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteExploreStringBuilder
    {
        internal static string AddExploreQuery(string idPlayer, string idFollower, string idLocation, string idEvent, int idStep, DateTime dateNextStep)
        {
            return $"INSERT INTO Explore (idPlayer, idFollower, idLocation, idEvent, idStep, dateNextStep) VALUES " +
                $"('{idPlayer}', '{idFollower}', '{idLocation}', '{idEvent}', '{idStep}', '{dateNextStep}')";
        }
    }
}
