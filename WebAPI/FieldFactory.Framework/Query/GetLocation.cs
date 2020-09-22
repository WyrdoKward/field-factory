using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class GetLocation
    {
        public int LocationId { get; set; }

        public GetLocation(int idLocation)
        {
            if (idLocation <= 0)
                throw new ArgumentOutOfRangeException("idLocation");

            LocationId = idLocation;
        }
    }
}
