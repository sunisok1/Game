using System;
using System.Collections.Generic;
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
                cardMono.CardStatus = cardFilter(card) ? CardStatus.Selectable : CardStatus.Unselectable;
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
                    foreach ((var card, var cardMono) in monoMapping)
                    {
                        if (selectedCards.Contains(card))
                            continue;
                        cardMono.CardStatus = CardStatus.Unselectable;
                    }
                }
            }
            else
            {
                selectedCards.Remove(selectingCard);
                if (selectedCards.Count == selectLimit - 1)
                {
                    foreach ((var card, var cardMono) in monoMapping)
                    {
                        if (selectedCards.Contains(card))
                            continue;
                        cardMono.CardStatus = cardFilter(card) ? CardStatus.Selectable : CardStatus.Unselectable;
                    }
                }
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