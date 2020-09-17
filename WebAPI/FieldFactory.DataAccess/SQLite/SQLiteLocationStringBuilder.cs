namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteLocationStringBuilder
    {
        internal static string SelectLocationByIdQuery(int idLocation)
        {
            return $"SELECT * FROM Location WHERE idLocation = {idLocation}";
        }
    }
}
