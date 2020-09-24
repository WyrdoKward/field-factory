using FieldFactory.DataAccess.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Core.Secutity
{
    public class AuthInteractor
    {
        PlayerProvider playerProvider = new PlayerProvider();

        public string GetLoginToken(string idPlayer)
        {
            var player = playerProvider.Get(idPlayer);
            return player.Token;
        }

        public string LoginPlayer(string idPlayer, string mdp)
        {
            string token = string.Empty;
            //hasher le mdp
            var player = playerProvider.GetWithPassword(idPlayer, mdp);
            if(player != null)
            {
                token = GenerateToken();
                playerProvider.SetToken(player.IdPlayer, token);
            }
            return token;
        }

        private string GenerateToken()
        {
            // TODO Stocker une durée de validité dans le token ?
            // https://stackoverflow.com/questions/14643735/how-to-generate-a-unique-token-which-expires-after-24-hours
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return token;
        }
    }
}
