using System;

namespace FieldFactory.Framework.Query
{
    public class GetPlayerHeadquartersQuery
    {
		public string IdPlayer { get; set; }


		// Toujours recevoir des strings ?
        public GetPlayerHeadquartersQuery(string idPlayer)
        {
			IdPlayer = idPlayer ?? throw new ArgumentOutOfRangeException(nameof(idPlayer));

        }
    }
}
