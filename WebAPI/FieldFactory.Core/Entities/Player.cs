using FieldFactory.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Entities
{
    public class Player
    {
        public string IdPlayer { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public Player()
        {

        }


        public Player(PlayerDTO dto)
        {
            IdPlayer = dto.IdPlayer;
            Email = dto.Email;
            Token = dto.Token;
        }
        /// <summary>
        /// Remove critical fields to send a safe player to the client
        /// </summary>
        public void Sanitize()
        {
            Token = "";
        }
    }
}
