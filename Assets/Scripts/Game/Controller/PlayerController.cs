using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Abstract;
using Game.Core.Map;
using Game.Core.Units;
using UnityEngine;
using Game.Mono.GameUI;
using Game.Mono;
using Game.Core.Cards;

namespace Game.Controller
{
    public class PlayerController : IController
    {
        private static HandCardContainer HandCardContainer => HandCardContainer.Instance;
        private TaskCompletionSource<bool> phaseUseTask;
        private Player player;
        public PlayerController(Player player)
        {
            this.player = player;
        }
        public async Task ChooseToUse(EventArgs args)
        {
            if (args is not UseCardArgs useCardArgs) return;
            var player = useCardArgs.player;
            var tcs = new TaskCompletionSource<bool>();
            foreach (var card in player.handCards)
            {
                HandCardContainer.SetCardUseable(card, useCardArgs.filterCard(card, player, EventArgs.Empty));
            }
        }

        public async Task<Vector2Int> SelectPosition(List<Vector2Int> posList)
        {
            var tcs = new TaskCompletionSource<bool>();
            var res = Vector2Int.zero;

            using (new GridStatusSetter(GridStatus.Selectable, posList))
            {
                ChessMono.Instance.OnGridClicked += OnGridClicked;
                await tcs.Task;
                ChessMono.Instance.OnGridClicked -= OnGridClicked;
            }

            return res;

            void OnGridClicked(Vector2Int position)
            {
                res = position;
                tcs.SetResult(true);
            }
        }

        public void EndUse()
        {
            phaseUseTask.SetResult(true);
        }

        public async Task PhaseUse()
        {
            phaseUseTask = new TaskCompletionSource<bool>();
            await phaseUseTask.Task;
        }
    }
}