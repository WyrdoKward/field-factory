using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Map.Event
{
    public class Outcome
    {
        public int Weight; // TODO si null => 1
        public int NextStepId; // si null => fin de quete

    }
}
