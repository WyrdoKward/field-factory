using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.DTO
{
    public class ExploreDTO
    {
        public string IdPlayer { get; set; }
        public string IdFollower { get; set; }
        public string IdLocation { get; set; }
        public string IdEvent { get; set; }
        public int IdStep { get; set; }
        public DateTime DateNextStep { get; set; }

        public ExploreDTO(string idPlayer, string idFollower, string idLocation, string idEvent, int idStep, DateTime dateNextStep)
        {
            IdPlayer = idPlayer;
            IdFollower = idFollower;
            IdLocation = idLocation;
            IdEvent = idEvent;
            IdStep = idStep;
            DateNextStep = dateNextStep;
        }
    }
}
