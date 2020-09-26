using System;

namespace FieldFactory.Framework.Query
{
    public class GetLocationWithActions
    {
        public string LocationId { get; set; }

        public GetLocationWithActions(string idLocation)
        {
            if (string.IsNullOrEmpty(idLocation))
                throw new ArgumentOutOfRangeException("idLocation");

            LocationId = idLocation;
        }
    }
}
