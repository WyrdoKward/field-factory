using System;

namespace FieldFactory.Core.Entities.Verbs
{
    public class Explore
    {
        public string IdPlayer { get; set; }
        public string IdFollower { get; set; }
        public string IdLocation { get; set; }
        public string IdEvent { get; set; }
        public int IdStep { get; set; }
        public DateTime DateNextStep { get; set; }
    }
}
