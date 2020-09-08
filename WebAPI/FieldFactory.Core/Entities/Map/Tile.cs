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

        public string LandType { get; set; }

        public Location Location { get; set; }

        public void GenerateLandType(ELandType model, int variation)
        {
            string path = "tile_" + model.ToString() + "_green_" + variation;

            if (path.Contains("ocean"))
                path.Replace("green", "dark");

            if (path.Contains("moor"))
                path.Replace("green", "blue");

            LandType = path;
        }
    }
}
