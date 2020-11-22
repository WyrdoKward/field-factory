using System;

namespace FieldFactory.DataAccess.DTO
{
    public class ExploreDTO
    {
        public string IdPlayer { get; set; }
        public string IdFollower { get; set; }
        public string IdLocation { get; set; }
        public string IdEvent { get; set; }
        public int IdStep { get; set; }
        public int? IdChoice { get; set; }
        public DateTime DateNextStep { get; set; }

        public string StepHistory { get; set; }

        public ExploreDTO(string idPlayer, string idFollower, string idLocation, string idEvent, int idStep, int? idChoice, DateTime dateNextStep, string stepHistory)
        {
            IdPlayer = idPlayer;
            IdFollower = idFollower;
            IdLocation = idLocation;
            IdEvent = idEvent;
            IdStep = idStep;
            IdChoice = idChoice;
            DateNextStep = dateNextStep;
            StepHistory = stepHistory;
        }
    }
}
