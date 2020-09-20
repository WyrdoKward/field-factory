namespace FieldFactory.DataAccess.DTO
{
    public class FollowerDTO
    {
        public string Id { get; set; }
        public string Json { get; set; }

        public FollowerDTO(string idFollower, string json)
        {
            Id = idFollower;
            Json = json;
        }
    }
}
