using FieldFactory.Core.Entities.Map.Event;
using FieldFactory.Core.Entities.Verbs;
using FieldFactory.Core.Map;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FieldFactory.Core.Verbs
{
    public class ExploreInteractor
    {
        ExploreProvider exploreProvider = new ExploreProvider();
        EventProvider eventProvider = new EventProvider();

        EventInteractor eventInteractor = new EventInteractor();

        public EventStep AddNewExploration(Explore exploration)
        {
            var tuple = eventInteractor.GetRandomEventForLocation(exploration.IdLocation);

            exploration.IdEvent = tuple.Item1;

            var nextStep = tuple.Item2.Steps[0];
            nextStep.Sanitize();
            exploration.Event.Steps.Add(nextStep);                
            exploration.DateNextStep = DateTime.Now.AddMinutes(nextStep.DurationInMin);

            exploreProvider.Add(exploration.ConvertToDTO());

            return exploration.Event.Steps[0];
        }

        public Explore GetExplorationForLocation(string idPlayer, string idLocation)
        {
            var exploreDto = exploreProvider.Get(idPlayer, idLocation);
            var explore = new Explore(exploreDto);
            var eventDto = eventProvider.Get(exploreDto.IdEvent);
            var evt = JsonConvert.DeserializeObject<Event>(eventDto.Json);
            explore.Event.Title = evt.Title; //On envoie que le title de l'objet Event car on ne veut pas donner tous les stpes au user, il se content de son history issu du ExploreDto.StepsHistory

            return explore;
        }

        public Explore ProcessChoice(int idChoice, Explore exploration)
        {
            // On récupère l'explo du joueur sur cette location
            var oldExplore = GetExplorationForLocation(exploration.IdPlayer, exploration.IdLocation);
            //var currentStep = oldExplore.GetCurrentStep();

            if (!oldExplore.IsFinished())
                throw new Exception("Le step n'est pas encore terminé");
            

            // On récupère l'event pour pouvoir piocher le step courant complet ainsi que le step suivant
            var eventDTO = eventProvider.Get(oldExplore.IdEvent);
            var evt = JsonConvert.DeserializeObject<Event>(eventDTO.Json);

            var currentStep = evt.Steps.Where(s => s.Id == oldExplore.IdStep).FirstOrDefault();

            //On vérifie que le choix envoyé existe dans le step courrant
            if (!currentStep.IsChoiceInputValid(idChoice))
                throw new Exception("Step pas valide");

            // On extrait le step suivant à partir du choix du player
            var userChoice = currentStep.Choices.Where(c => c.Id == idChoice).FirstOrDefault();
            int randomeOutcome = userChoice.ChooseRandomNextStep();
            var nextStep = evt.Steps.Where(s => s.Id == randomeOutcome).FirstOrDefault(); 
            
            //On met à jour l'explo en BDD avec le nouveau Step et timer
            exploration.IdFollower = oldExplore.IdFollower;
            exploration.IdEvent = oldExplore.IdEvent;
            exploration.IdStep = nextStep.Id;
            exploration.DateNextStep = DateTime.Now.AddMinutes(nextStep.DurationInMin);
            exploration.Event.Steps = oldExplore.Event.Steps;

            // TODO Sanitize aussi le current step pour enlever les choices pas faits ( != idChoice)?
            nextStep.Sanitize();
            exploration.Event.Steps.Add(nextStep);

            if(nextStep.Choices == null)
            {
                //Fin de quete => Delete Explore au lieu d'update ?
            }

            exploreProvider.Update(exploration.ConvertToDTO());

            return exploration;

        }
    }
}
