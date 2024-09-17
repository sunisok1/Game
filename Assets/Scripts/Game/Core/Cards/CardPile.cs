using System.Collections.Generic;
using System.Linq;
using Framework.Singletons;
using Game.Abstract;

namespace Game.Core.Cards
{
    public class CardPile : Singleton<CardPile>
    {
        private readonly LinkedList<AbstractCard> cards = new();

        public void Init(params IEnumerable<AbstractCard>[] cardLists)
        {
            foreach (var cardList in cardLists)
            {
                foreach (var card in cardList)
                {
                    cards.AddLast(card);
                }
            }
        }

        public AbstractCard GetFromTop()
        {
            var card = cards.First.Value;
            cards.RemoveFirst();
            return card;
        }

        public List<AbstractCard> GetFromTop(int count)
        {
            var result = new List<AbstractCard>();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                result.Add(cards.First.Value);
                cards.RemoveFirst();
            }
            return result;
        }

        public AbstractCard GetFromBottom()
        {
            var card = cards.Last.Value;
            cards.RemoveLast();
            return card;
        }

        public List<AbstractCard> GetFromBottom(int count)
        {
            var result = new List<AbstractCard>();
            for (int i = 0; i < count && cards.Count > 0; i++)
            {
                result.Add(cards.Last.Value);
                cards.RemoveLast();
            }
            return result;
        }
    }
}
