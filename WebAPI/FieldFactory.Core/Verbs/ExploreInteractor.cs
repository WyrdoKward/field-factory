using FieldFactory.Core.Entities.Verbs;
using FieldFactory.Core.Map;
using FieldFactory.DataAccess.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Verbs
{
    public class ExploreInteractor
    {
        ExploreProvider exploreProvider = new ExploreProvider();

        public void AddNewExploration(Explore exploration)
        {
            EventInteractor eventGetter = new EventInteractor();
            var tuple = eventGetter.GetRandomEventForLocation(exploration.IdLocation);

            exploration.IdEvent = tuple.Item1;
            exploration.Steps.Add(tuple.Item2.Steps[0]);                
            exploration.DateNextStep = DateTime.Now.AddMinutes(1);

            exploreProvider.Add(exploration.ConvertToDTO());
        }
    }
}
