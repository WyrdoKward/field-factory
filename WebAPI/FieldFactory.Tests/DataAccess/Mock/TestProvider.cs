using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Tests.DataAccess.Mock
{
    public class TestProvider : SQLiteBaseProvider
    {
        public Dictionary<int, Dictionary<string, string>> ReadColumnsMock(string query)
        {
            return ReadColumns(query, 3);
        }
    }
}
