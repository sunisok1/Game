using System.Collections.Generic;
using System.Linq;
using Framework.Singletons;
using Game.Abstract;

namespace Game.Core.Cards
{
    public class CardPile : Singleton<CardPile>
    {
        private readonly LinkedList<Card> cards = new();

        public void Init(params IEnumerable<Card>[] cardLists)
        {
            foreach (var cardList in cardLists)
            {
                foreach (var card in cardList)
                {
                    cards.AddLast(card);
                }
            }
        }

        public Card GetFromTop()
        {
            var card = cards.First.Value;
            cards.RemoveFirst();
            return card;
        }

        public List<Card> GetFromTop(int count)
        {
            var result = new List<Card>();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                result.Add(cards.First.Value);
                cards.RemoveFirst();
            }
            return result;
        }

        public Card GetFromBottom()
        {
            var card = cards.Last.Value;
            cards.RemoveLast();
            return card;
        }

        public List<Card> GetFromBottom(int count)
        {
            var result = new List<Card>();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                result.Add(cards.Last.Value);
                cards.RemoveLast();
            }
            return result;
        }
    }
}
