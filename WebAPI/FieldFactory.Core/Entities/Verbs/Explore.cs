using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.DataAccess.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FieldFactory.Core.Entities.Verbs
{
    public class Explore
    {
        public string IdPlayer { get; set; }
        public string IdFollower { get; set; }
        public string IdLocation { get; set; }
        public string IdEvent { get; set; }

        /// <summary>
        /// Le Step courrant
        /// </summary>
        public int IdStep { get; set; }
        public DateTime DateNextStep { get; set; }
        
        public Event Event { get; set; }

        public Explore()
        {
            IdStep = 0;
        }

        public Explore(ExploreDTO dto)
        {
            IdPlayer = dto.IdPlayer;
            IdFollower = dto.IdFollower;
            IdLocation = dto.IdLocation;
            IdEvent = dto.IdEvent;
            IdStep = dto.IdStep;
            DateNextStep = dto.DateNextStep;
            Event = new Event();
            Event.Steps = JsonConvert.DeserializeObject<List<EventStep>>(dto.StepHistory);
        }

        public ExploreDTO ConvertToDTO()
        {
            var steps = JsonConvert.SerializeObject(Event.Steps);
            return new ExploreDTO(IdPlayer, IdFollower, IdLocation, IdEvent, IdStep, DateNextStep, steps);
        }

        /// <summary>
        /// Returns true if DateNextStep is over, meaning that the step is complete
        /// </summary>
        public bool IsFinished()
        {
            return DateTime.Compare(DateNextStep, DateTime.Now) < 0;
        }
    }
}
