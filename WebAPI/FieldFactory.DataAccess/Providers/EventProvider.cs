using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;

namespace FieldFactory.DataAccess.Providers
{
    public class EventProvider : SQLiteBaseProvider
    {
        public EventDTO Get(int idEvent)
        {
            var query = SQLiteLocationStringBuilder.SelectLocationByIdQuery(idEvent);
            var @event = Read(query);
            return new EventDTO();
        }
    }
}
