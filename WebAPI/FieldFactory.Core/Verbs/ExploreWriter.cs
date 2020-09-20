using FieldFactory.DataAccess.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Verbs
{
    public class ExploreWriter
    {
        ExploreProvider exploreProvider = new ExploreProvider();

        public void AddNewExploration(string idPlayer, string idFollower, string idLocation, string idEvent, int idStep, DateTime dateNextStep)
        {
            exploreProvider.Add(idPlayer, idFollower, idLocation, idEvent, idStep, dateNextStep);
        }
    }
}
