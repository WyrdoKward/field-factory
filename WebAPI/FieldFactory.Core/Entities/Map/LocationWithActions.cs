using FieldFactory.Core.Entities.Verbs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities.Map
{
    /// <summary>
    /// Représente un Location avec toutes les actions en cours dessus du joueur
    /// </summary>
    public class LocationWithActions
    {
        public Location Location { get; set; }
        public Explore Explore { get; set; }

        /// <summary>
        /// Supprime les données sensibles avant de renvoyer l'objet
        /// </summary>
        public void Sanitize()
        {
            Location.RandomEvents = null;
        }
    }
}
