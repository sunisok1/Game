using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Core.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public sealed class Chess
    {
        private class PlayerList : IReadOnlyCollection<Player>
        {
            private readonly LinkedList<Player> playersLinkList = new();

            public PlayerList(IEnumerable<Player> players)
            {
                foreach (var player in players)
                {
                    playersLinkList.AddLast(player);
                }
            }

            public Player First => playersLinkList.First.Value;

            public Player GetNextPlayer(Player player)
            {
                LinkedListNode<Player> listNode = playersLinkList.Find(player);
                return listNode == null ? null : (listNode.Next ?? playersLinkList.First).Value;
            }

            public IEnumerator<Player> GetEnumerator()
            {
                return playersLinkList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return playersLinkList.GetEnumerator();
            }

            public int Count => playersLinkList.Count;
        }

        private readonly PlayerList playerList;

        public Chess(Status status)
        {
            playerList = new(status.players);
        }

        public async Task StartAsync()
        {
            await SceneManager.LoadSceneAsync("_Scenes/Chess");
            Debug.Log("Scene Chess Entered");
            PlayerManager.InitPlayers(playerList);
            Debug.Log("PlayerManager.InitPlayers(playerList);");
            await PhaseLoop(playerList.First);
        }

        private async Task PhaseLoop(Player p)
        {
            await p.PhaseAsync();
        }
    }
}