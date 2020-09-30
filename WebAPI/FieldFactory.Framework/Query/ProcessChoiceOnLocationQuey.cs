using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class ProcessChoiceOnLocationQuey
    {
        public string IdPlayer { get; set; }
        public string IdLocation { get; set; }
        public int NextStepId { get; set; }

        public ProcessChoiceOnLocationQuey(string idPlayer, string idLocation, int nextStepId)
        {
            IdPlayer = idPlayer;
            IdLocation = idLocation;
            NextStepId = nextStepId;
        }
    }
}
