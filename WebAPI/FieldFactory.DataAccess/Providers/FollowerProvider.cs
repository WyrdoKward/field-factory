using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;

namespace FieldFactory.DataAccess.Providers
{
    public class FollowerProvider : SQLiteBaseProvider
    {
        public FollowerDTO Get(string idFollower)
        {
            var query = SQLiteFollowerQueryBuilder.SelectFollowerByIdQuery(idFollower);
            var follower = ReadScalar(query);
            return new FollowerDTO(idFollower, follower.ToString());
        }
    }
}
