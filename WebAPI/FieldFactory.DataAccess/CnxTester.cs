using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess
{
    public class CnxTester
    {
        SqliteConnectionStringBuilder ConnectionStringBuilder;
        public CnxTester()
        {

            ConnectionStringBuilder = new SqliteConnectionStringBuilder();
            //Use DB in project directory.  If it does not exist, create it:
            ConnectionStringBuilder.DataSource = "./field-factory.db";
        }

        private void ExecuteSingleNonQuery(string sqlCommand)
        {
            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = sqlCommand;
                cmd.ExecuteNonQuery();
            }
        }

        private void ExecuteTransaction(List<string> transactionLines)
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

        private List<string> Read(string selectQuery)
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

        public void CreateTable()
        {
            ExecuteSingleNonQuery("DROP TABLE IF EXISTS favorite_beers");
            ExecuteSingleNonQuery("CREATE TABLE favorite_beers(name VARCHAR(50))");
        }

        public void InsertBeers()
        {
            List<string> commands = new List<string>()
            {
                "INSERT INTO favorite_beers VALUES('Chouffe')",
                "INSERT INTO favorite_beers VALUES('Tripel K')",
                "INSERT INTO favorite_beers VALUES('Gulden Draak')",
                "INSERT INTO favorite_beers VALUES('Dorelei')",

            };
            ExecuteTransaction(commands);
        }

        public List<string> GetBeers()
        {
            return Read("SELECT name FROM favorite_beers");
        }
        
        public void TestCnx()
        {

            using (var connection = new SqliteConnection(ConnectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Create a table (drop if already exists first):

                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS favorite_beers";
                delTableCmd.ExecuteNonQuery();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE favorite_beers(name VARCHAR(50))";
                createTableCmd.ExecuteNonQuery();

                //Seed some data:
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertCmd.CommandText = "INSERT INTO favorite_beers VALUES('LAGUNITAS IPA')";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO favorite_beers VALUES('JAI ALAI IPA')";
                    insertCmd.ExecuteNonQuery();

                    insertCmd.CommandText = "INSERT INTO favorite_beers VALUES('RANGER IPA')";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                //Read the newly inserted data:
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT name FROM favorite_beers";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }


            }
        }
        }
    }
