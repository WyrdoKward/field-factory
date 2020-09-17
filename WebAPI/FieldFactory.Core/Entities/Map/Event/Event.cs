using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Map.Event
{
    public class Event
    {
        public int Id;
        public string Title;

        public List<EventStep> Steps;
    }
}
