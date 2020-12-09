using FieldFactory.Core.Entities.Headquarters;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Headquarters
{
    public class FurnitureInteractor
    {
        FurnitureProvider furnitureProvider = new FurnitureProvider();
        //PlayerHqProvider playerHqProvider = new PlayerHqFurnitureProvider();

        public List<Furniture> GetPlayerFurnitures(string playerId)
        {
            //Récupérer la ligne qui correspond aux constructions faites par la player dans son HQ
            //var playerHqDto = playerHqProvider.Get(playerId);

            //On récupère toutes les fournitures construites dans le HQ
            /*var furnitureDto = furnitureProvider.Get(playerId);
            var furniture = JsonConvert.DeserializeObject<Furniture>(furnitureDto.Json);*/

            //Pour chaque furniture on récupère la description qui correspond à son level
            List<Furniture> furnitures = null;

            return furnitures;
        }
    }
}
