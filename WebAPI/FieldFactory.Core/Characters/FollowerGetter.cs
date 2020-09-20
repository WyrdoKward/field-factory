using FieldFactory.Core.Entities;
using FieldFactory.DataAccess.Providers;
using Newtonsoft.Json;

namespace FieldFactory.Core.Characters
{
    public class FollowerGetter
    {
        FollowerProvider followerProvider = new FollowerProvider();

        public Follower GetFollowerById(string idFollower)
        {
            var followerDto = followerProvider.Get(idFollower);
            var follower = JsonConvert.DeserializeObject<Follower>(followerDto.Json);

            return follower;
        }
    }
}
