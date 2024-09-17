using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Singletons;
using Game.Abstract;
using Game.Core.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core.Turn
{

    public sealed class TurnSystem : Singleton<TurnSystem>
    {
        private readonly CancellationTokenSource cancellationTokenSource = new();
        public Player CurPlayer { get; private set; }
        public event Action<Player> OnPlayerTuenEnter;
        private readonly LinkedList<Player> playerQueue = new();

        public async void StartAsync(Status status)
        {
            var playerList = status.players;
            PlayerManager.InitPlayers(playerList);

            try
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    await TurnLoop(cancellationTokenSource.Token);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async Task TurnLoop(CancellationToken cancellationToken)
        {
            playerQueue.Clear();
            foreach (var item in PlayerManager.PlayerList)
            {
                playerQueue.AddLast(item);
            }

            while (playerQueue.Count > 0)
            {
                var playerNode = playerQueue.First;
                playerQueue.RemoveFirst();
                await PhaseLoop(playerNode.Value, cancellationToken);
            }
        }

        private async Task PhaseLoop(Player p, CancellationToken cancellationToken)
        {
            if (p == null || cancellationToken.IsCancellationRequested)
            {
                return;
            }

            CurPlayer = p;

            if (p.Equals(PlayerManager.First))
            {
                Debug.Log("TurnNum++");
            }
            p.InitPhase();
            OnPlayerTuenEnter?.Invoke(p);

            await Task.Delay(1000, cancellationToken);
            await p.PhaseAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
            // StopPhaseLoop
            cancellationTokenSource.Cancel();
        }
    }
}