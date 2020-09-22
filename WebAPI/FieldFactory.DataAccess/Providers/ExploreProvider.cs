﻿using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.Providers
{
    public class ExploreProvider : SQLiteBaseProvider
    {
        private const int NB_COL_IN_TABLE = 6;
        public void Add(ExploreDTO exploration)
        {
            var query = SQLiteExploreStringBuilder.AddExploreQuery(exploration);
            ExecuteSingleNonQuery(query);
        }


        /*private List<ExploreDTO> ConvertIntoDto(Dictionary<int, List<string>> rawLocations)
        {
            var res = new List<ExploreDTO>();

            foreach (var rawLocation in rawLocations)
            {
                res.Add(new ExploreDTO() { Id = int.Parse(rawLocation.Value[0]), Name = rawLocation.Value[1], Json = rawLocation.Value[2] });
            }

            return res;
        }*/
    }
}
