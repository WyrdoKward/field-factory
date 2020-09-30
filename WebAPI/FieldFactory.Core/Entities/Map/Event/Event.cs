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

        public bool IsNextStepInputValid(int currentStep, int nextStepInput)
        {
            List<int> possibleNextSteps = Steps.Where(s => s.Id == currentStep).FirstOrDefault().Choices.Select(c => c.NextStepId).ToList();
            return possibleNextSteps.Contains(nextStepInput);
        }
    }
}
