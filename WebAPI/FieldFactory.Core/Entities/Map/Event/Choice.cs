using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FieldFactory.Core.Entities.Map.Event
{
    public class Choice
    {
        public int Id;
        public string Text;

        public Outcome[] Outcomes;



        /// <summary>
        /// Renvoie un nextStepId choisi au hazard selon le poids des outcomes
        /// </summary>
        public int ChooseRandomNextStep(int? forcedRandomIndex = null)
        {
            int sumOfOutcomesWeight = Outcomes.Sum(o => o.Weight);
            int[] nextStepIdsWeightedArray = new int[sumOfOutcomesWeight];

            int i = 0;
            foreach (Outcome o in Outcomes)
            {
                for (int j = 0; j < o.Weight; j++)
                {
                    nextStepIdsWeightedArray[i] = o.NextStepId;
                    i++;
                }
            }

            Random rnd = new Random();
            int indexInArray = forcedRandomIndex ?? rnd.Next(sumOfOutcomesWeight);

            return nextStepIdsWeightedArray[indexInArray];

        }


        /// <summary>
        /// Vide les éléments qu'on ne veut pas montrer à l'utilisateur
        /// </summary>
        internal void Sanitize()
        {
            Outcomes = null;
        }
    }
}
