using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteFurnitureQueryBuilder
    {
        internal static string SelectFurnitureByIdQuery(string idFurniture)
        {
            return $"SELECT json FROM Event WHERE idEvent = '{idFurniture}'";
        }
    }
}
