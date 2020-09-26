using FieldFactory.Core.Characters;
using FieldFactory.Core.Entities;
using FieldFactory.Framework.Authorizer;

namespace FieldFactory.Framework.Executor
{
    public class FollowerExecutor : BaseExecutor
    {

        public FollowerExecutor(Identity identity)
        {
            Identity = identity;
        }
        /// <summary>
        /// Get a follower based on id
        /// </summary>
        /// <param name="idFollower"></param>
        /// <returns></returns>
        public Follower Execute(string idFollower)
        {
            //Validation ?
            //A-t-il débloqué ce follower ?


            FollowerGetter followerGetter = new FollowerGetter();
            var follower = followerGetter.GetFollowerById(idFollower);

            return follower;
        }
    }
}
