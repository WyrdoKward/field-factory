using FieldFactory.Core.Entities.Headquarters;
using FieldFactory.DataAccess.DTO;
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
        PlayerHeadquartersProvider playerHqProvider = new PlayerHeadquartersProvider();

        public List<Furniture> GetPlayerFurnitures(string playerId)
        {
            //Récupérer la ligne qui correspond aux constructions faites par la player dans son HQ
            var playerHqDto = playerHqProvider.Get(playerId);
            var playerHQ = new PlayerHeadquarters(playerHqDto);

            Dictionary<string, int> playerHqFurnitures = playerHQ.ConstructedFurnitures();

            //On récupère toutes les fournitures construites dans le HQ
            List<FurnitureDTO> furnituresDto = furnitureProvider.GetAll();
            List<Furniture> furnitures = new List<Furniture>();

            foreach (var dto in furnituresDto)
            {
                //filtrer sur celles que possède le player
                if (!playerHqFurnitures.ContainsKey(dto.Id))
                    continue;

                var furniture = JsonConvert.DeserializeObject<Furniture>(dto.Json, new Newtonsoft.Json.Converters.StringEnumConverter());
                furniture._currentLevel = playerHqFurnitures[dto.Id];
                furniture.RemoveNotCurrentLevels();
                //récupérer que le furniturelevel construit ?
                furnitures.Add(furniture);
            }

            return furnitures;
        }
    }
}
