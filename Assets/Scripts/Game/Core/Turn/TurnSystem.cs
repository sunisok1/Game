using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.Singletons;
using Game.Core.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core.Turn
{
    public sealed class TurnSystem : Singleton<TurnSystem>
    {
        private readonly CancellationTokenSource cancellationTokenSource = new();

        public Player CurPlayer { get; private set; }
        public event Action<Player> OnPlayerPhaseEnter;

        public async void StartAsync(Status status)
        {
            var playerList = status.players;
            await SceneManager.LoadSceneAsync("_Scenes/Chess");
            Debug.Log("Scene Chess Entered");
            PlayerManager.InitPlayers(playerList);
            Debug.Log("PlayerManager.InitPlayers(playerList);");

            try
            {
                await PhaseLoop(PlayerManager.First, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("PhaseLoop canceled.");
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

            OnPlayerPhaseEnter?.Invoke(p);

            await Task.Delay(1000, cancellationToken);
            await p.PhaseAsync();
            await PhaseLoop(p.Next, cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
            // StopPhaseLoop
            cancellationTokenSource.Cancel();
        }
    }
}