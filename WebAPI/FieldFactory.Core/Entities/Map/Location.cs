using FieldFactory.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Map
{
    public class Location
    {
        public int Id;
        public string Title;
        public string Description;
        public ELocationType LocationType;
        public EVerb[] Verbs;

        public Location() { }

        public Location(int id)
        {
            //GetLocationbyId(id); depuis le conteneur
        }
    }
}
