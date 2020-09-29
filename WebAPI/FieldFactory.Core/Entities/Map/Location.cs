using FieldFactory.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FieldFactory.Core.Entities.Map
{
    public class Location
    {
        //Ajouter string IdLocation
        public int Id;
        public string Title;
        public string Description;

        public ELocationType LocationType { get; set; }

        public EVerb[] Verbs { get; set; }

        [JsonIgnore]
        public List<string> RandomEvents { get; set; }

        public Location() { }

        public string GetARandomEvent()
        {
            Random rnd = new Random();
            int e = rnd.Next(RandomEvents.Count - 1);
            return RandomEvents[e];
        }

    }
}
