
using Game.Abstract;
using Game.Core.Units;

namespace Game.Core.Cards
{

    public abstract class Card
    {
        protected Card(string name, Suit suit, int point)
        {
            this.name = name;
            this.suit = suit;
            this.point = point;
        }
        public readonly string name;
        public readonly int point;
        public readonly Suit suit;

        public CardColor Color
        {
            get
            {
                return suit switch
                {
                    Suit.Spade => CardColor.Black,
                    Suit.Heart => CardColor.Red,
                    Suit.Club => CardColor.Black,
                    Suit.Diamond => CardColor.Red,
                    _ => CardColor.None,
                };
            }
        }
        public abstract CardType Type { get; }
        public virtual bool Enable { get; } = false;
        public virtual int Usable { get; } = int.MaxValue;

        public virtual bool Range(Card card, Player player, Player target)
        {
            return true;
        }

        public virtual bool FilterTarget(Card card, Player player, Player target)
        {
            return true;
        }

        public virtual void Content()
        {

        }
    }
}