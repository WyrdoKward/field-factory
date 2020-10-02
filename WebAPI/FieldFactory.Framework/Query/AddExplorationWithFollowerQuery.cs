using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class AddExplorationWithFollowerQuery
    {
        //Y'aura surement pas tout ces champs dans la query, certains seront récupérés en base ? ou dans la session (idPlayer...)
        public string IdPlayer { get; }
        public string IdFollower { get; }
        public string IdLocation { get; }

        public AddExplorationWithFollowerQuery(string idPlayer, string idFollower, string idLocation)
        {
            IdPlayer = idPlayer ?? throw new ArgumentNullException(nameof(idPlayer));
            IdFollower = idFollower ?? throw new ArgumentNullException(nameof(idFollower));
            IdLocation = idLocation ?? throw new ArgumentNullException(nameof(idLocation));
        }

    }
}
