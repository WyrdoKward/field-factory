using FieldFactory.Core.Entities.Headquarters;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;

namespace FieldFactory.Core.Headquarters
{
    public class PlayerHeadquartersInteractor
    {
        PlayerHeadquartersProvider playerHeadquartersProvider = new PlayerHeadquartersProvider();

        public PlayerHeadquarters GetPlayerHeadquarters(string idPlayer)
        {
            var playerHeadquartersDto = playerHeadquartersProvider.Get(idPlayer);
			var playerHeadquarters = new PlayerHeadquarters(playerHeadquartersDto);

			
            return playerHeadquarters;
        }       
    }
}
