﻿using System;

namespace FieldFactory.Framework.Query
{
    public class GetLocation
    {
        public string LocationId { get; set; }

        public GetLocation(string idLocation)
        {
            if (string.IsNullOrEmpty(idLocation))
                throw new ArgumentOutOfRangeException("idLocation");

            LocationId = idLocation;
        }
    }
}
