using FieldFactory.Core.Secutity;
using FieldFactory.Framework.Authorizer;
using FieldFactory.Framework.Query;
using System;

namespace FieldFactory.Framework.Executor
{
    public class AuthExecutor : BaseExecutor
    {
        public AuthExecutor(Identity identity)
        {
            Identity = identity;
        }

        public string Execute(LoginQuery query)
        {
            //Validation ?
            //Droit d'accéder à cette lcoation ?

            // TODO créer une vraie exception
            AuthInteractor authInteractor = new AuthInteractor();
            var token = authInteractor.LoginPlayer(query.IdPlayer, query.Mdp);
            if (string.IsNullOrEmpty(token))
                throw new Exception("Login failed");

            return token;
        }
    }
}
