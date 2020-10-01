using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class ProcessChoiceOnLocationQuey
    {
        public string IdPlayer { get; set; }
        public string IdLocation { get; set; }
        public int IdChoice { get; set; }

        public ProcessChoiceOnLocationQuey(string idPlayer, string idLocation, int idChoice)
        {
            IdPlayer = idPlayer;
            IdLocation = idLocation;
            IdChoice = idChoice;
        }
    }
}
