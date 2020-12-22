using Newtonsoft.Json;
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

        public void RemoveNotCurrentLevels()
        {
            var tmpList = new List<FurnitreLevel>(Levels);
            foreach (var l in tmpList)
            {
                if(l.Level != _currentLevel)
                {
                    Levels.Remove(l);
                }
            }
            
        }

        [JsonIgnore]
        public int _currentLevel;

    }
}
