namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLitePlayerStringBuilder
    {
        internal static string SelectPlayerByIdQuery(string idPlayer)
        {
            return $"SELECT * FROM Player WHERE idPlayer = '{idPlayer}'";
        }
    }
}
