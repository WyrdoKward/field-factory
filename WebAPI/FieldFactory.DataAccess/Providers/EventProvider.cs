using FieldFactory.DataAccess.DTO;
using FieldFactory.DataAccess.SQLite;

namespace FieldFactory.DataAccess.Providers
{
    public class EventProvider : SQLiteBaseProvider
    {
        public EventDTO Get(string idEvent)
        {
            var query = SQLiteEventQueryBuilder.SelectEventByIdQuery(idEvent);
            var evt = ReadScalar(query);
            return new EventDTO(idEvent, evt.ToString());
        }
    }
}
