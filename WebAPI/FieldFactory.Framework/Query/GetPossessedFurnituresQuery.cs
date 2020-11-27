using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class GetPossessedFurnituresQuery
    {
        public string IdPlayer { get; }
        public GetPossessedFurnituresQuery(string idPlayer)
        {
            IdPlayer = idPlayer ?? throw new ArgumentNullException(nameof(idPlayer));
        }
    }
}
