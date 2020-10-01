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
            exploration.Event.Steps.Add(tuple.Item2.Steps[0]);                
            exploration.DateNextStep = DateTime.Now.AddMinutes(tuple.Item2.Steps[0].DurationInMin);

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

        public Explore ProcessChoice(Explore exploration)
        {
            // On récupère l'explo du joueur sur cette location
            var oldExplore = GetExplorationForLocation(exploration.IdPlayer, exploration.IdLocation);

            if(!oldExplore.IsFinished())
                throw new Exception("Le step n'est pas encore terminé");


            // On récupère l'event et on extrait le step choisi par le player
            var eventDTO = eventProvider.Get(oldExplore.IdEvent);
            var evt = JsonConvert.DeserializeObject<Event>(eventDTO.Json);
            var nextStep = evt.Steps.Where(s => s.Id == exploration.IdStep).FirstOrDefault();

            //On vérifie que le nexStep envoyé est bien un step possible
            if (!evt.IsNextStepInputValid(oldExplore.IdStep, exploration.IdStep))
                throw new Exception("Step pas valide");

            //On met à jour l'explo en BDD avec le nouveau Step et timer
            exploration.IdFollower = oldExplore.IdFollower;
            exploration.IdEvent = oldExplore.IdEvent;
            exploration.DateNextStep = DateTime.Now.AddMinutes(nextStep.DurationInMin);
            exploration.Event.Steps = oldExplore.Event.Steps;
            exploration.Event.Steps.Add(nextStep);

            exploreProvider.Update(exploration.ConvertToDTO());

            return exploration;

        }
    }
}
