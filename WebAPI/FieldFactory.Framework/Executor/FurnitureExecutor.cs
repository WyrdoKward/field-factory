using FieldFactory.Core.Entities;
using FieldFactory.Core.Entities.Headquarters;
using FieldFactory.Core.Headquarters;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;
using System;
using System.Collections.Generic;

namespace FieldFactory.Framework.Executor
{
    public class FurnitureExecutor : BaseExecutor
    {
        FurnitureInteractor furnitureInteractor = new FurnitureInteractor();

        public FurnitureExecutor(Identity identity)
        {
            Identity = identity;
        }


        public List<Furniture> Execute(GetPossessedFurnituresQuery query)
        {
            return furnitureInteractor.GetPlayerFurnitures(query.IdPlayer);
        }
    }
}
