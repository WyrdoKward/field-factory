using FieldFactory.Core.Verbs;
using FieldFactory.Framework.Query;

namespace FieldFactory.Framework.Executor
{
    public class ExploreExecutor
    {
        public void Execute(AddExplorationWithFollowerQuery query)
        {
            ExploreWriter exploreWriter = new ExploreWriter();
            exploreWriter.AddNewExploration(query.IdPlayer, query.IdFollower, query.IdLocation, query.IdEvent, query.IdStep, query.DateNextStep);
        }
    }
}
