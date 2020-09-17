namespace FieldFactory.DataAccess.DTO
{
    public class EventDTO
    {
        public string Id { get; set; }
        public string Json { get; set; }

        public EventDTO(string idEvent, string json)
        {
            Id = idEvent;
            Json = json;
        }
    }
}
