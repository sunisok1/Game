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

        private Player player;
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

        public void SetCardUseable(Card card, bool useable)
        {
            if (monoMapping.TryGetValue(card, out var cardMone))
            {
                cardMone.SetUseable(useable);
            }
        }

        private void OnPlayerTurnEnter(Player player)
        {
            this.player = player;
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
                cardMono.SetUseable(player.GetCardUseable(card));
                monoMapping.Add(card, cardMono);
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