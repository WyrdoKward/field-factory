using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.Providers
{
    public class FurnitureProvider : SQLiteBaseProvider
    {
        public FurnitureDTO Get(string idFurniture)
        {
            var query = SQLiteFurnitureQueryBuilder.SelectFurnitureByIdQuery(idFurniture);
            var json = ReadScalar(query);
            return new FurnitureDTO(idFurniture, json.ToString());
        }
    }
}
