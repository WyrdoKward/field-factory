using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Map.Event
{
    public class EventStep
    {
        public int Id;
        public string Text;

        public List<Choice> Choices;
        public List<Outcome> Outcomes;

        //Duration ?
        public int DurationInMin = 5;
    }
}
