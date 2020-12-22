using FieldFactory.Core.Entities.Headquarters;
using FieldFactory.Core.Headquarters;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;

namespace FieldFactory.Framework.Executor
{
    public class PlayerHeadquartersExecutor : BaseExecutor
    {
        PlayerHeadquartersInteractor playerHeadquartersInteractor = new PlayerHeadquartersInteractor();
		FurnitureInteractor furnitureInteractor = new FurnitureInteractor();


        public PlayerHeadquartersExecutor(Identity identity)
        {
            Identity = identity;
        }

        public PlayerHeadquarters Execute(GetPlayerHeadquartersQuery query)
        {
            return playerHeadquartersInteractor.GetPlayerHeadquarters(query.IdPlayer);
        }
    }
}
