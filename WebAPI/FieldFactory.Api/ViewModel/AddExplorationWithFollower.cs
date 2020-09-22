using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldFactory.Api.ViewModel
{
    public class AddExplorationWithFollower
    {
        public string IdPlayer { get; set; }
        public string IdFollower { get; set; }
        public string IdLocation { get; set; }
        public string IdEvent { get; set; }
    }
}