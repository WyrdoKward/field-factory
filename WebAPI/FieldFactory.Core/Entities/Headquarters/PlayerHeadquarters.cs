using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.DataAccess.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldFactory.Core.Entities.Headquarters
{
    public class PlayerHeadquarters
    {
        #region Members
        public string IdPlayer { get; set; }
		public string IdHeadquarters { get; set; }
		public int DummyFurniture { get; set; }
		public int Watchtower { get; set; }

		#endregion

        public PlayerHeadquarters()
        {
        }
		
		public PlayerHeadquarters(PlayerHeadquartersDTO dto)
        {
			IdPlayer = dto.IdPlayer;
			IdHeadquarters = dto.IdHeadquarters;
			DummyFurniture = dto.DummyFurniture;
			Watchtower = dto.Watchtower;

		}
		
		public PlayerHeadquartersDTO ConvertToDTO()
		{
			return new PlayerHeadquartersDTO(IdPlayer, IdHeadquarters, DummyFurniture, Watchtower);
		}

		/// <summary>
		/// Gives currently built Furnitures for the player
		/// </summary>
		internal Dictionary<string, int> ConstructedFurnitures()
		{
			return new Dictionary<string, int>() {
				{"dummyFurniture", DummyFurniture },
				{"watchtower", Watchtower } 
			};
		}
	}
}
