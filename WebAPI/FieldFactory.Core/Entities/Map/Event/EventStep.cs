using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FieldFactory.Core.Entities.Map.Event
{
    public class EventStep
    {
        public int Id;
        public string Text;

        public List<Choice> Choices;

        //Duration ?
        public int DurationInMin = 5;

        /// <summary>
        /// Vérifie l'existence d'un id de Choice pour ce step
        /// </summary>
        /// <param name="choiceInput"></param>
        /// <returns></returns>
        public bool IsChoiceInputValid(int choiceInput)
        {
            int[] possibleChoices = Choices.Select(c => c.Id).ToArray();
            return possibleChoices.Contains(choiceInput);
        }

        /// <summary>
        /// Ne garde que le choice fait pour l'historique, et vide les éléments qu'on ne veut pas montrer à l'utilisateur (outcomes..)
        /// </summary>
        public void Sanitize()
        {
            if (Choices != null)
            {
                foreach (var c in Choices)
                {
                    c.Sanitize();
                }
            }
        }
    }
}
