using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteFurnitureQueryBuilder
    {
        internal static string SelectFurnitureByIdQuery(string idFurniture)
        {
            return $"SELECT json FROM Furniture WHERE idFurniture = '{idFurniture}'";
        }

        internal static string SelectAllFurnitures()
        {
            return $"SELECT idFurniture, json FROM Furniture";
        }
    }
}
