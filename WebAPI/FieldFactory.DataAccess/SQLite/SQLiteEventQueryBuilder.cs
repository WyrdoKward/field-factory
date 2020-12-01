namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteEventQueryBuilder
    {
        internal static string SelectEventByIdQuery(string idEvent)
        {
            return $"SELECT json FROM Event WHERE idEvent = '{idEvent}'";
        }
    }
}
