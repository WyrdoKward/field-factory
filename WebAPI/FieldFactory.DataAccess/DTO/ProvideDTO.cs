using System;

namespace FieldFactory.DataAccess.DTO
{
    public class ProvideDTO
    {
        public string IdPlayer { get; set; }
        public string IdFurniture { get; set; }
        public int NewLevel { get; set; }
        public DateTime DateEnd { get; set; }

        public ProvideDTO(string idPlayer, string idFurniture, int newLevel, DateTime dateEnd)
        {
            IdPlayer = idPlayer;
            IdFurniture = idFurniture;
            NewLevel = newLevel;
            DateEnd = dateEnd;
        }
    }
}
