using System;
using System.Collections.Generic;

namespace Game.Core.Units
{
    public static class PlayerManager
    {
        private static List<Player> playerList;
        public static event Action<Player> OnPlayerCreate;
        public static Player First => playerList[0];

        public static void InitPlayers(List<Player> playerList)
        {
            if (playerList.Count < 2)
            {
                throw new Exception("playerList.Count<2,InitPlayers failed!");
            }

            PlayerManager.playerList = playerList;

            LinkPlayers();

            foreach (var player in playerList)
            {
                OnPlayerCreate?.Invoke(player);
            }

            return;

            void LinkPlayers()
            {
                playerList[0].NextSeat = playerList[1];
                playerList[0].PreviousSeat = playerList[^1];
                playerList[0].Next = playerList[1];
                playerList[0].Previous = playerList[^1];

                for (int i = 1; i < playerList.Count - 1; i++)
                {
                    playerList[i].NextSeat = playerList[i + 1];
                    playerList[i].PreviousSeat = playerList[i - 1];
                    playerList[i].Next = playerList[i + 1];
                    playerList[i].Previous = playerList[i - 1];
                }

                playerList[^1].NextSeat = playerList[0];
                playerList[^1].PreviousSeat = playerList[^2];
                playerList[^1].Next = playerList[0];
                playerList[^1].Previous = playerList[^2];
            }
        }
    }
}