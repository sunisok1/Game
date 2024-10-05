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
using System.Linq;

namespace Game.Controller
{
    public class PlayerController : IController
    {
        private TaskCompletionSource<bool> phaseUseTask;
        private TaskCompletionSource<bool> useCardTask;
        private readonly List<Player> selectedTargets = new();
        private Player player;
        public PlayerController(Player player)
        {
            this.player = player;
        }
        public async Task ChooseToUse(EventArgs args)
        {
            // if (args is not UseCardArgs useCardArgs) return;
            // var player = useCardArgs.player;
            // var tcs = new TaskCompletionSource<bool>();
            // foreach (var card in player.handCards)
            // {
            //     HandCardContainer.SetCardUseable(card, useCardArgs.filterCard(card, player, EventArgs.Empty));
            // }
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
            phaseUseTask = new();
            HandCardContainer.Instance.OnSelectedCardsFull += UseCard;
            HandCardContainer.Instance.OnSelectedCardsNotFull += CancelUseCard;
            await phaseUseTask.Task;
            HandCardContainer.Instance.OnSelectedCardsFull -= UseCard;
            HandCardContainer.Instance.OnSelectedCardsNotFull -= CancelUseCard;
        }

        private async void UseCard(List<Card> list)
        {
            var tcs = new TaskCompletionSource<bool>();

            PlayerButtonManager.Instance.ShowConfirmButton(() => tcs.SetResult(true));
            PlayerButtonManager.Instance.ShowCancelButton(() => tcs.SetResult(false));

            var card = list.First();
            var result = await SelectTargetAsyc((player) => card.Range(this.player, player) && card.FilterTarget(this.player, player), card.TargetNum, tcs);

            if (result)
            {
                Debug.Log($"use {card.name} to target {selectedTargets.First().name}");
                card.Content(selectedTargets);
            }

            ChessMono.Instance.ClearSelectedPlayers();
            HandCardContainer.Instance.ClearSelectedCards();
        }

        private async Task<bool> SelectTargetAsyc(Func<Player, bool> filter, IntRange targetNum, TaskCompletionSource<bool> tcs)
        {
            useCardTask = new();


            ChessMono.Instance.FilterSelectablePlayers(filter);
            PlayerMono.OnPlayerSelected += OnPlayerSelected;

            return await tcs.Task;

            void OnPlayerSelected(Player player, bool selected)
            {
                if (selected)
                {
                    selectedTargets.Add(player);
                }
                else
                {
                    selectedTargets.Remove(player);
                }

                bool targetsSelectedOver = targetNum.InRange(selectedTargets.Count);
                UpdateTargetsSelectedOverStatus(targetsSelectedOver);
            }

            void UpdateTargetsSelectedOverStatus(bool over)
            {
                PlayerButtonManager.Instance.SetConfirmInteractable(over);
            }
        }

        private void CancelUseCard()
        {
            useCardTask?.SetCanceled();
        }

    }
}