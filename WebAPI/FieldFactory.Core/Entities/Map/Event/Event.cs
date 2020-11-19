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

        public EventStep GetStepByIdOrDefault(int idStep)
        {
            if(idStep < 0)
                idStep = 0;

            return Steps.Where(s => s.Id == idStep).FirstOrDefault();
        }

    }
}
