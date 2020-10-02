namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteLocationQueryBuilder
    {
        internal static string SelectLocationByIdQuery(string idLocation)
        {
            return $"SELECT * FROM Location WHERE idLocation = '{idLocation}'";
        }
    }
}
