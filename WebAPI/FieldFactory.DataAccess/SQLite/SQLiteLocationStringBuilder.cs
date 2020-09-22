namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteLocationStringBuilder
    {
        internal static string SelectLocationByIdQuery(string idLocation)
        {
            return $"SELECT * FROM Location WHERE idLocation = '{idLocation}'";
        }
    }
}
