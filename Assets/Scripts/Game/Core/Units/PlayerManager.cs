using System;
using System.Collections.Generic;

namespace Game.Core.Units
{
    public static class PlayerManager
    {
        public static event Action<Player> OnPlayerCreate;

        public static void InitPlayer(Player player)
        {
            OnPlayerCreate?.Invoke(player);
        }

        public static void InitPlayers(IEnumerable<Player> playerList)
        {
            foreach (var player in playerList)
            {
                InitPlayer(player);
            }
        }
    }
}