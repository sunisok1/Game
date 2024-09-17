
namespace Game.Abstract
{

    public enum Suit
    {
        None,
        Spade,
        Heart,
        Club,
        Diamond
    }
    public abstract class AbstractCard
    {
        public readonly string name;
        public readonly int point;
        public readonly Suit suit;
        protected AbstractCard(string name, Suit suit, int point)
        {
            this.name = name;
            this.suit = suit;
            this.point = point;
        }
    }
}