using FieldFactory.Core.Entities;
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
            var playerDto = playerProvider.GetById(idPlayer);
            return playerDto.Token;
        }

        public Player LoginPlayer(string idPlayer, string mdp)
        {
            string token = string.Empty;
            //hasher le mdp
            var playerDto = playerProvider.GetWithPassword(idPlayer, mdp);
            if(playerDto != null)
            {
                playerDto.Token = GenerateToken();
                playerProvider.SetToken(playerDto.IdPlayer, playerDto.Token);
            }
            return new Player(playerDto);
        }

        public Player GetPlayerFromToken(string token)
        {
            var playerDto = playerProvider.GetByToken(token);
            return new Player(playerDto);
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
