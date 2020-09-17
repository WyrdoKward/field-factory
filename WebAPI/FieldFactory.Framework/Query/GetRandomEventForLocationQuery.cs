using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class GetRandomEventForLocationQuery
    {
        public int LocationId { get; set; }

        public GetRandomEventForLocationQuery(int idLocation)
        {
            if (idLocation <= 0)
                throw new ArgumentOutOfRangeException("idLocation");
            
            LocationId = idLocation;
        }
    }
}
