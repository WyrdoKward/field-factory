using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class AddExplorationWithFollowerQuery
    {
        //Y'aura surement pas tout ces champs dans la query, certains seront récupérés en base ? ou dans la session (idPlayer...)
        public string IdPlayer { get; }
        public string IdFollower { get; }
        public string IdLocation { get; }
        public string IdEvent { get; }
        public int IdStep { get; }
        public DateTime DateNextStep { get; }

        public AddExplorationWithFollowerQuery(string idPlayer, string idFollower, string idLocation, string idEvent, int idStep, DateTime dateNextStep)
        {
            if (idStep < 0)
                throw new ArgumentOutOfRangeException("idStep");

            //vérifier la comparaison de date que a ne soit pas antérieur à la date du jour
            if(dateNextStep.CompareTo(DateTime.Now) <= 0)
                throw new ArgumentOutOfRangeException("dateNextStep");

            IdPlayer = idPlayer ?? throw new ArgumentNullException(nameof(idPlayer));
            IdFollower = idFollower ?? throw new ArgumentNullException(nameof(idFollower));
            IdLocation = idLocation ?? throw new ArgumentNullException(nameof(idLocation));
            IdEvent = idEvent ?? throw new ArgumentNullException(nameof(idEvent));
            IdStep = idStep;
            DateNextStep = dateNextStep;
        }

    }
}
