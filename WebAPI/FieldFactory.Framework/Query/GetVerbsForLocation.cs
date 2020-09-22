using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class GetVerbsForLocation
    {
        public int LocationId { get; set; }

        public GetVerbsForLocation(int idLocation)
        {
            if (idLocation <= 0)
                throw new ArgumentOutOfRangeException("idLocation");

            LocationId = idLocation;
        }
    }
}
