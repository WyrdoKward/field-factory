using FieldFactory.Core.Entities.Verbs;
using FieldFactory.Core.Verbs;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;

namespace FieldFactory.Framework.Executor
{
    public class ExploreExecutor : BaseExecutor
    {
        public ExploreExecutor(Identity identity)
        {
            Identity = identity;
        }

        public void Execute(AddExplorationWithFollowerQuery query)
        {
            ExploreInteractor exploreWriter = new ExploreInteractor();
            Explore exploration = new Explore()
            {
                IdPlayer = query.IdPlayer,
                IdFollower = query.IdFollower,
                IdLocation = query.IdLocation
            };
            exploreWriter.AddNewExploration(exploration);
        }
    }
}
