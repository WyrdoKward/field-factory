using FieldFactory.Core.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Map
{
    public class Tile
    {
        public int Id;
        public int X;
        public int Y;

        public ELandType LandType { get; set; }

        public Location Location { get; set; }

    }
}
