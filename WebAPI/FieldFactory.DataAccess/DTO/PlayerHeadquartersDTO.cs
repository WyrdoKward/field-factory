using System;

namespace FieldFactory.DataAccess.DTO
{
    public class PlayerHeadquartersDTO
    {
        public string IdPlayer { get; set; }
		public string IdHeadquarters { get; set; }
		public int DummyFurniture { get; set; }
		public int Watchtower { get; set; }


        public PlayerHeadquartersDTO(string idPlayer, string idHeadquarters, int dummyFurniture, int watchtower)
        {
            IdPlayer = idPlayer;
			IdHeadquarters = idHeadquarters;
			DummyFurniture = dummyFurniture;
			Watchtower = watchtower;

        }
    }
}
