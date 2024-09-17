using System;
using System.Collections.Generic;

namespace Game.Core.Units
{
    public static class PlayerManager
    {
        #region  PublicProperties
        public static Player First => playerList[0];
        public static IReadOnlyList<Player> PlayerList => playerList;
        #endregion
        private static List<Player> playerList;
        public static event Action<Player> OnPlayerCreate;
        public static void InitPlayers(List<Player> playerList)
        {
            if (playerList.Count < 2)
            {
                throw new Exception("playerList.Count<2,InitPlayers failed!");
            }

            PlayerManager.playerList = playerList;

            foreach (var player in playerList)
            {
                OnPlayerCreate?.Invoke(player);
            }
        }
    }
}