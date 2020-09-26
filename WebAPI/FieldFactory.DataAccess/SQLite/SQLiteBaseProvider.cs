using Microsoft.Data.Sqlite;
using System;
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

        /// <summary>
        /// Exécute une unique requete de modification
        /// </summary>
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

        /// <summary>
        /// Exécute une suite de requetes de modification
        /// </summary>
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

        /// <summary>
        /// Retourne sous form de string un objet complet en BDD
        /// </summary>
        /// <param name="selectQuery">Ne marche que pour les SELECT *</param>
        /// <param name="nbCols">Le nombre de colunes de la table en BDD</param>
        /// <returns></returns>
        internal Dictionary<int, List<string>> ReadColumns(string selectQuery, int nbCols = 1)
        {
            // int = num Row
            // List<string> = les val des cols
            Dictionary<int, List<string>> res = new Dictionary<int, List<string>>();
            int row = 0;

            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = selectQuery;

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(row, new List<string>());
                        for (int i = 0; i < nbCols; i++)
                        {
                            var rawRes = reader.IsDBNull(i) ? "" : reader.GetString(i);
                            res[row].Add(rawRes);
                        }
                        row++;
                    }
                }
            }

            return res;
        }

        internal object ReadScalar(string selectQuery)
        {
            object res;
            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = selectQuery;

                res = selectCmd.ExecuteScalar();
            }

            return res;
        }
    }
}
