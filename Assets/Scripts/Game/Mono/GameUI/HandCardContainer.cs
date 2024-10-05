using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Singletons;
using Game.Core.Cards;
using Game.Core.Turn;
using Game.Core.Units;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class HandCardContainer : MonoSingleton<HandCardContainer>
    {
        [SerializeField] private Transform content;
        [SerializeField] private CardMono cardMonoPrefab;

        private readonly Dictionary<Card, CardMono> monoMapping = new();
        private readonly List<Card> selectedCards = new();
        private int selectLimit = 1;
        private Func<Card, bool> cardFilter;
        public event Action<List<Card>> OnSelectedCardsFull;
        public event Action OnSelectedCardsNotFull;
        private void Start()
        {
            TurnSystem.Instance.OnPlayerTurnEnter += OnPlayerTurnEnter;
            TurnSystem.Instance.OnPlayerTurnExit += OnPlayerTurnExit;
        }

        private void OnDestroy()
        {
            TurnSystem.Instance.OnPlayerTurnEnter -= OnPlayerTurnEnter;
            TurnSystem.Instance.OnPlayerTurnExit -= OnPlayerTurnExit;
        }

        private void OnPlayerTurnEnter(Player player)
        {
            cardFilter = player.GetCardUseable;
            monoMapping.Clear();
            content.DestoryAllChildren();
            CreateHandCard(player.handCards);

            player.OnGainHandCard += CreateHandCard;
            player.OnLoseHandCard += DestoryHandCard;
        }
        private void OnPlayerTurnExit(Player player)
        {
            player.OnGainHandCard -= CreateHandCard;
            player.OnLoseHandCard -= DestoryHandCard;

        }

        private void CreateHandCard(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                var cardMono = Instantiate(cardMonoPrefab, content);
                cardMono.Init(card);
                cardMono.Selectable = cardFilter(card);
                cardMono.OnCardSelected += OnCardSelected;
                monoMapping.Add(card, cardMono);
            }
        }

        private void OnCardSelected(Card selectingCard, bool selected)
        {
            if (selected)
            {
                selectedCards.Add(selectingCard);
                if (selectedCards.Count == selectLimit)
                {
                    UpdateCardsStatus(true);
                    OnSelectedCardsFull?.Invoke(selectedCards);
                }
            }
            else
            {
                selectedCards.Remove(selectingCard);
                if (selectedCards.Count == selectLimit - 1)
                {
                    UpdateCardsStatus(false);
                    OnSelectedCardsNotFull?.Invoke();
                }
            }

            void UpdateCardsStatus(bool isSelectedFull)
            {

                foreach ((var card, var cardMono) in monoMapping)
                {
                    if (selectedCards.Contains(card))
                        continue;
                    if (isSelectedFull)
                        cardMono.Selectable = false;
                    else
                        cardMono.Selectable = cardFilter(card);
                }
            }
        }

        public void SelectCard(Card card) => monoMapping[card].Selected = true;
        public void DeselectCard(Card card) => monoMapping[card].Selected = false;
        public void ClearSelectedCards()
        {
            while (selectedCards.Count > 0)
            {
                DeselectCard(selectedCards.First());
            }
        }
        private void DestoryHandCard(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                if (monoMapping.TryGetValue(card, out var cardMono))
                {
                    Destroy(cardMono.gameObject);
                    monoMapping.Remove(card);
                }
            }
        }
    }
}