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

        LocationInteractor locationInteractor = new LocationInteractor();
        EventInteractor eventInteractor = new EventInteractor();

        public Explore AddNewExploration(Explore exploration)
        {
            var location = locationInteractor.GetLocation(exploration.IdLocation);
            // Récupérer les coords de la locaiton sur la map pour calculer le travelTime
            int travelTime = locationInteractor.ProcessTravelTime();

            exploration.IdEvent = eventInteractor.GetRandomEventForLocation(exploration.IdLocation);
            exploration.IdStep = -1;
            
            exploration.SetDateNextStep(DateTime.Now.AddSeconds(travelTime));

            exploreProvider.Add(exploration.ConvertToDTO());

            return exploration;
        }

        /// <summary>
        /// Récupère une Exploration à une location donnée
        /// </summary>
        /// <param name="exploration"></param>
        /// <returns></returns>
        public Explore GetExploreForLocation(Explore queryExploration)
        {
            var exploreDto = exploreProvider.Get(queryExploration.IdPlayer, queryExploration.IdLocation);
            var explore = new Explore(exploreDto);

            return explore;
        }

        /// <summary>
        /// Récupère l'exploration correspondante et enregistre le choix du user et la date du nextStep
        /// </summary>
        /// <param name="queryExploration">Explore from query with IdPlayer, IdLocation, IdChoice</param>
        /// <returns>Explore with selected choice and new Date for timer</returns>
        public Explore RegisterEventChoiceOnLocation(Explore queryExploration)
        {
            var exploreDto = exploreProvider.Get(queryExploration.IdPlayer, queryExploration.IdLocation);
            var explore = new Explore(exploreDto);

            // Si on envoie un choix trop tot ou qu'on est pas encore au step 0, on renvoit l'objet tel quel sans maj
            // TODO you renvoyer exception car on n'a pas fait le put ? l'objet existe de toutes façons déjà coté client
            if (!explore.IsFinished() || explore.IdStep < 0) // encapsuler dans un validator ? enum pour donner le statut de manière claire et masquer els conditions ?
                return explore;

            explore.UpdateDateWithChoice(queryExploration.IdChoice);

            //On save en BDD
            exploreProvider.Update(explore.ConvertToDTO());
            
            return explore;
        }

        public Explore GetExplorationForLocation(string idPlayer, string idLocation)
        {
            //explore + event.json tel qu'en BDD
            var exploreDto = exploreProvider.Get(idPlayer, idLocation);
            var explore = new Explore(exploreDto);
            var eventDto = eventProvider.Get(exploreDto.IdEvent);
            var evt = JsonConvert.DeserializeObject<Event>(eventDto.Json);
            explore.Event.Title = evt.Title; //On envoie que le title de l'objet Event car on ne veut pas donner tous les stpes au user, il se contente de son history issu du ExploreDto.StepsHistory

            if (explore.IsFinished() && explore.IdStep < 0)
            {
                //Si c'est fini et qu'on est pas encore au premier step
                var nextStep = evt.Steps.Where(s => s.Id == 0).FirstOrDefault();
                nextStep.Sanitize();
                explore.Event.Steps.Add(nextStep);
                explore.IdStep = 0;

                exploreProvider.Update(explore.ConvertToDTO());
            }
            else if(explore.IsFinished() && explore.IdChoice != null)
            {
                //on process le choice et on affiche le step suivant selon l'outcome
                explore = ProcessEventChoice(explore, evt);
            }
            // Et si c'est pas fini, ou qu'on est dans un step sans choice (step0) on renvoit l'objet tel qu'en BDD


            return explore;
        }

        /// <summary>
        /// Selectionne une output selon le choix stocké en BDD du user et Update, ou bien Delete l'Explore si il n'y a plus de choices
        /// </summary>
        private Explore ProcessEventChoice(Explore exploration, Event evt) {

            var currentStep = evt.GetStepByIdOrDefault(exploration.IdStep);
                

            //On marque le choix effectué
            exploration.Event.Steps.Last().Choices.Where(c => c.Id == exploration.IdChoice).FirstOrDefault().IsSelected = true;

            //On vérifie que le choix envoyé existe dans le step courrant
            if (!currentStep.IsChoiceInputValid(exploration.IdChoice))
                throw new Exception("Choice pas valide");

            // On extrait le step suivant à partir du choix du player
            var userChoice = currentStep.Choices.Where(c => c.Id == exploration.IdChoice).FirstOrDefault();
            int randomNextStepId = userChoice.ChooseRandomNextStep();
            var nextStep = evt.GetStepByIdOrDefault(randomNextStepId);


            //On met à jour l'explo en BDD avec le nouveau Step et timer
            exploration.IdStep = nextStep.Id;
            exploration.SetDateNextStep(DateTime.Now.AddMinutes(nextStep.DurationInMin));
            exploration.Event.Steps = exploration.Event.Steps; // ???

            // TODO Sanitize aussi le current step pour enlever les choices pas faits ( != idChoice)? ou trouver un moyen de savoir quel choix a été fait par le user ? => bool "selected" a coté de "Id", "Text" et "Outcomes"
            nextStep.Sanitize();
            exploration.Event.Steps.Add(nextStep);

            if(nextStep.Choices == null)                
                exploreProvider.Delete(exploration.IdPlayer, exploration.IdLocation); //Fin de quete => Delete Explore au lieu d'update // Logguer l'expédition passée qqpart ?
            else
                exploreProvider.Update(exploration.ConvertToDTO());

            return exploration;

        }
    }
}
