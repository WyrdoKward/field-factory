using System;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLitePlayerQueryBuilder
    {
        internal static string SelectPlayerByIdQuery(string idPlayer)
        {
            return $"SELECT * FROM Player WHERE idPlayer = '{idPlayer}'";
        }

        internal static string SelectPlayerByTokenQuery(string token)
        {
            return $"SELECT * FROM Player WHERE token = '{token}'";
        }

        internal static string SelectPlayerWithPwdQuery(string idPlayer, string hashPwd)
        {
            return $"SELECT * FROM Player WHERE idPlayer = '{idPlayer}' AND hashpwd = '{hashPwd}'";
        }

        internal static string SetTokenQuery(string idPlayer, string token)
        {
            return $"UPDATE Player SET token = '{token}' WHERE idPlayer = '{idPlayer}'";
        }

    }
}
