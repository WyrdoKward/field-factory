using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace FieldFactory.DataAccess.SQLite
{
    public class SQLiteBaseProvider
    {
        SqliteConnectionStringBuilder ConnectionStringBuilder;
        public SQLiteBaseProvider()
        {

            ConnectionStringBuilder = new SqliteConnectionStringBuilder();
            //Use DB in project directory.  If it does not exist, create it:
            ConnectionStringBuilder.DataSource = "./field-factory.db";
        }

        internal void ExecuteSingleNonQuery(string sqlCommand)
        {
            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = sqlCommand;
                cmd.ExecuteNonQuery();
            }
        }

        internal void ExecuteTransaction(List<string> transactionLines)
        {
            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var cmd = connection.CreateCommand();
                    foreach (var line in transactionLines)
                    {
                        cmd.CommandText = line;
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        internal List<string> Read(string selectQuery)
        {
            List<string> res = new List<string>();

            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = selectQuery;

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(reader.GetString(0));
                    }
                }
            }

            return res;
        }
    }
}
