using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Headquarters
{
    public class Furniture
    {
        public int Id;
        public string Title;
        public string Description;

        public List<FurnitreLevel> Levels;


        public int GetMaxLevel() {
            return Levels.Count;
        }

    }
}
