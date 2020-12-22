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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerFurnitures"></param>
        /// <returns></returns>
        public List<FurnitureDTO> GetAll()
        {
            List<FurnitureDTO> res = new List<FurnitureDTO>();
            var query = SQLiteFurnitureQueryBuilder.SelectAllFurnitures();
            var jsonLines = ReadJsonLines(query);

            foreach (var line in jsonLines)
            {
                res.Add(new FurnitureDTO(line.Key, line.Value));
            }

            return res;
        }
    }
}
