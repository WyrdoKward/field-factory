using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class GetRandomEventForLocationQuery
    {
        public string LocationId { get; set; }

        public GetRandomEventForLocationQuery(string idLocation)
        {            
            LocationId = idLocation;
        }
    }
}
