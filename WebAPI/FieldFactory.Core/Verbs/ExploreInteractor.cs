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
            exploration.Steps.Add(eventGetter.GetRandomEventForLocation(exploration.IdLocation));
            exploration.DateNextStep = DateTime.Now.AddMinutes(1);
            exploration.IdStep = 0;

            exploreProvider.Add(exploration.ConvertToDTO());
        }
    }
}
