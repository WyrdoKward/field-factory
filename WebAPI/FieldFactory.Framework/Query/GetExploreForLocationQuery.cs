using System;

namespace FieldFactory.Framework.Query
{
    public class GetExploreForLocationQuery
    {
        public string IdPlayer { get; }
        public string IdLocation { get; }

        public GetExploreForLocationQuery(string idPlayer, string idLocation)
        {
            IdPlayer = idPlayer ?? throw new ArgumentNullException(nameof(idPlayer));
            IdLocation = idLocation ?? throw new ArgumentNullException(nameof(idLocation));
        }
    }
}
