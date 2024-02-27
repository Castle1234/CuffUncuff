using CitizenFX.Core;
using System;
using CitizenFX.Core.Native;
using System.Collections;

namespace CuffUncuff
{
    public class Main : BaseScript
    {
        [Command("cuff")]
        [Command("uncuff")]
        
        public void GetClosestPlayer()
        {
            Player closestPlayer = null;
            float closestDistance = -1f;
            // loop through all players
            foreach (Player player in Players.())
            {
                if (player == Game.Player)
                {
                    continue;
                }

                var ped = player.Character;
                var playerCoords = ped.Position;

                var distance = Vector3.Distance(Game.PlayerPed.Position, playerCoords);

                if (closestPlayer == null || distance < closestDistance)
                {
                    closestPlayer = player;
                    closestDistance = distance;
                }
            }

            if (closestPlayer != null)
            {
                CuffCommand();
            }
        }
        private void CuffCommand()
        {
            var policePlayer = Game.PlayerPed;
            var cuffedPlayer = Game.PlayerPed.Handle;
            bool isCuffed = API.IsPedCuffed(cuffedPlayer);
            //Check if the player is cuffed already
            //Get the player closest to the police player
            
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
