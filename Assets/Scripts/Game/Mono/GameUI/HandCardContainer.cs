using System;
using System.Collections.Generic;
using Framework;
using Game.Abstract;
using Game.Core.Turn;
using Game.Core.Units;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class HandCardContainer : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private CardMono cardMonoPrefab;

        private readonly Dictionary<AbstractCard, CardMono> monoMapping = new();
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
            monoMapping.Clear();
            content.DestoryAllChildren();
            foreach (var card in player.handCards)
            {
                var cardMono = Instantiate(cardMonoPrefab, content);
                cardMono.Init(card);
            }

            player.OnGainHandCard += CreateHandCard;
            player.OnLoseHandCard += DestoryHandCard;
        }
        private void OnPlayerTurnExit(Player player)
        {
            player.OnGainHandCard -= CreateHandCard;
            player.OnLoseHandCard -= DestoryHandCard;

        }

        private void CreateHandCard(IEnumerable<AbstractCard> cards)
        {
            foreach (var card in cards)
            {
                var cardMono = Instantiate(cardMonoPrefab, content);
                cardMono.Init(card);
                monoMapping.Add(card, cardMono);
            }
        }

        private void DestoryHandCard(IEnumerable<AbstractCard> cards)
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