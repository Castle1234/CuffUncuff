using CitizenFX.Core;
using System;
using CitizenFX.Core.Native;
using System.Collections.Generic;

namespace CuffUncuff
{
    public class Main : BaseScript
    {
        [Command("cuff")]
        
        public void GetClosestPlayer()
        {
            Player closestPlayer = null;
            float closestDistance = -1f;
            // loop through all players
            foreach (Player player in Players)
            {
                if (player == Game.Player)
                {
                    continue;
                }

                // get the player's ped
                var ped = player.Character;
                var playerCoords = ped.Position;

                // get the distance between the player and the police player
                var distance = Vector3.Distance(Game.PlayerPed.Position, playerCoords);

                if (closestPlayer == null || distance < closestDistance)
                {
                    closestPlayer = player;
                    closestDistance = distance;
                }
            }

            // if a player was found, cuff them
            if (closestPlayer != null)
            {
                CuffCommand();
            }
        }
        private void CuffCommand()
        {
            var cuffedPlayer = Game.PlayerPed.Handle;
            bool isCuffed = API.IsPedCuffed(cuffedPlayer);
            //Check if the player is cuffed already
            
            switch (isCuffed)
            {
                case true:
                    {
                        //Uncuff the player
                        API.SetEnableHandcuffs(cuffedPlayer, false);
                        break;
                    }

                case false:
                    {
                        //Cuff the player
                        API.SetEnableHandcuffs(cuffedPlayer, true);
                        break;
                    }
            }
        }
    }
}
