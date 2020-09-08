using FieldFactory.Core.Enum;
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

        public ELandType LandType;

        public Location Location;
        
    }
}
