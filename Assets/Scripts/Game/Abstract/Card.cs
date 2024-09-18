
namespace Game.Abstract
{

    public abstract class AbstractCard
    {
        public readonly string name;
        public readonly int point;
        public readonly Suit suit;

        public CardColor color
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
        protected AbstractCard(string name, Suit suit, int point)
        {
            this.name = name;
            this.suit = suit;
            this.point = point;
        }
    }
}