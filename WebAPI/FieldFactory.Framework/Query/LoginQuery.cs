using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Query
{
    public class LoginQuery
    {
        public string IdPlayer { get; set; }
        public string Mdp { get; set; }

        public LoginQuery(string idPlayer, string mdp)
        {
            IdPlayer = IdPlayer;
            Mdp = mdp;
        }
    }
}
