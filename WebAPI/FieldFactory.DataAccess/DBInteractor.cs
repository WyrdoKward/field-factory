using FieldFactory.DataAccess.DTO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi
{
    public class DBInteractor : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = field-factory.db;");
        }
        internal DbSet<UserDTO> UserDTO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDTO>().HasData(
                new UserDTO() { Login = "login", Password = "123456Az", Email = "ert@ert.com" }
            );
        }

        public void TestCnx()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            //Use DB in project directory.  If it does not exist, create it:
            connectionStringBuilder.DataSource = "./field-factory.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
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