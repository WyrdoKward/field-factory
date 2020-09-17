using FieldFactory.Core.Enum;
using FieldFactory.DataAccess.DTO;

namespace FieldFactory.Core.Entities.Map
{
    public class Location
    {
        public int Id;
        public string Title;
        public string Description;

        public ELocationType LocationType { get; set; }

        public EVerb[] Verbs { get; set; }

        public Location() { }

        public Location(LocationDTO dto)
        {

        }
    }
}
