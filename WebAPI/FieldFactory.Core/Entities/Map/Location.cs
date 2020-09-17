using FieldFactory.Core.Enum;
using System;
using System.Collections.Generic;

namespace FieldFactory.Core.Entities.Map
{
    public class Location
    {
        public int Id;
        public string Title;
        public string Description;

        public ELocationType LocationType { get; set; }

        public EVerb[] Verbs { get; set; }

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
