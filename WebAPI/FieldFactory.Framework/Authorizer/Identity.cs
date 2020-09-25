using FieldFactory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Authorizer
{
    public class Identity
    {
        public Player Player { get; set; }

        public Identity(Player player)
        {
            Player = player;
        }
    }
}
