using System.Collections.Generic;
using System.Linq;
using Framework.Singletons;
using Game.Abstract;
using UnityEngine;

namespace Game.Core.Cards
{
    public class CardPile : Singleton<CardPile>
    {
        private readonly List<Card> cards = new();

        public void Init(params IEnumerable<Card>[] cardLists)
        {
            foreach (var cardList in cardLists)
            {
                foreach (var card in cardList)
                {
                    cards.Add(card);
                    Sheffle();
                }
            }
        }

        private void Sheffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                var random = Random.Range(0, cards.Count);
                (cards[i], cards[random]) = (cards[random], cards[i]);
            }
        }

        public Card GetFromTop()
        {
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public List<Card> GetFromTop(int count)
        {
            var result = new List<Card>();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                result.Add(cards[0]);
                cards.RemoveAt(0);
            }
            return result;
        }

        public Card GetFromBottom()
        {
            var card = cards[^1];
            cards.RemoveAt(cards.Count - 1);
            return card;
        }

        public List<Card> GetFromBottom(int count)
        {
            var result = new List<Card>();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                result.Add(cards[^1]);
                cards.RemoveAt(cards.Count - 1);
            }
            return result;
        }
    }
}
