using FieldFactory.Core.Enum;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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

    }
}
