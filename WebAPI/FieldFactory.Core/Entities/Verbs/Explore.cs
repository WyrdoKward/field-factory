using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.DataAccess.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldFactory.Core.Entities.Verbs
{
    public class Explore
    {
        #region Members
        public string IdPlayer { get; set; }
        public string IdFollower { get; set; }
        public string IdLocation { get; set; }
        public string IdEvent { get; set; }

        public Event Event { get; set; }
        /// <summary>
        /// Le Step courrant
        /// </summary>
        public int IdStep { get; set; }
        public int? IdChoice{ get; set; }

        /// <summary>
        /// Returns friendly string date format for serialization
        /// </summary>
        public string DateNextStep
        {
            get
            {
               return m_dateNextStep.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        #endregion

        #region Getters&Setters

        private DateTime m_dateNextStep;
        //Getter et Setter with DateTime
        public void SetDateNextStep(DateTime d)
        {
            m_dateNextStep = d;
        }

        public DateTime GetDateNextStep()
        {
            return m_dateNextStep;
        }


        #endregion

        public Explore()
        {
            IdStep = 0;
            Event = new Event();
        }

        public Explore(ExploreDTO dto)
        {
            IdPlayer = dto.IdPlayer;
            IdFollower = dto.IdFollower;
            IdLocation = dto.IdLocation;
            IdEvent = dto.IdEvent;
            IdStep = dto.IdStep;
            IdChoice = dto.IdChoice;
            SetDateNextStep(dto.DateNextStep);
            Event = new Event();
            Event.Steps = JsonConvert.DeserializeObject<List<EventStep>>(dto.StepHistory);
        }

        public ExploreDTO ConvertToDTO()
        {
            var steps = JsonConvert.SerializeObject(Event.Steps);
            return new ExploreDTO(IdPlayer, IdFollower, IdLocation, IdEvent, IdStep, IdChoice, GetDateNextStep(), steps);
        }

        /// <summary>
        /// Returns true if DateNextStep is over, meaning that the step is complete
        /// </summary>
        public bool IsFinished()
        {
            return DateTime.Compare(GetDateNextStep(), DateTime.Now) < 0;
        }

        public EventStep GetCurrentStep()
        {
            return Event.Steps.Last();
        }

        /// <summary>
        /// Quand l'utilisateur fait un choix, on l'enregistre et on maj la dateNextStep
        /// </summary>
        /// <param name="idChoice"></param>
        public void UpdateDateWithChoice(int? idChoice)
        {
            var currentStep = GetCurrentStep();
            DateTime nextDate = DateTime.Now.AddMinutes(currentStep.DurationInMin);

            IdChoice = idChoice;
            Event.Steps.Last().Choices.Where(c => c.Id == IdChoice).FirstOrDefault().IsSelected = true;

            SetDateNextStep(nextDate);
        }
    }
}
