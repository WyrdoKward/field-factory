using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FieldFactory.Core.Entities.Map.Event
{
    public class Event
    {
        public int Id;
        public string Title;

        public List<EventStep> Steps;

        public Event()
        {
            Steps = new List<EventStep>();
        }

    }
}
