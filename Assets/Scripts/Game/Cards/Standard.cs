using System.Collections.Generic;
using Game.Abstract;
using Game.Core.Cards;
using Game.Core.Units;

namespace Game.Cards
{
    public static class Standard
    {
        public static List<Card> cards = new()
        {
            new Sha(Suit.Spade,7),
            new Sha(Suit.Spade,8),
            new Sha(Suit.Spade,8),
            new Sha(Suit.Spade,9),
            new Sha(Suit.Spade,9),
            new Sha(Suit.Spade,10),
            new Sha(Suit.Spade,10),
            new Sha(Suit.Club,2),
            new Sha(Suit.Club,3),
            new Sha(Suit.Club,4),
            new Sha(Suit.Club,5),
            new Sha(Suit.Club,6),
            new Sha(Suit.Club,7),
            new Sha(Suit.Club,8),
            new Sha(Suit.Club,8),
            new Sha(Suit.Club,9),
            new Sha(Suit.Club,9),
            new Sha(Suit.Club,10),
            new Sha(Suit.Club,10),
            new Sha(Suit.Club,11),
            new Sha(Suit.Club,11),
            new Sha(Suit.Heart,10),
            new Sha(Suit.Heart,11),
            new Sha(Suit.Heart,11),
            new Sha(Suit.Diamond,6),
            new Sha(Suit.Diamond,7),
            new Sha(Suit.Diamond,8),
            new Sha(Suit.Diamond,9),
            new Sha(Suit.Diamond,10),
            new Sha(Suit.Diamond,13),
        };
    }

    public class Sha : Card
    {
        public Sha(Suit suit, int point) : base("sha", suit, point)
        {
        }

        public override bool Range(Player player, Player target)
        {
            return player.InRange(target);
        }

        public override bool FilterTarget(Player player, Player target)
        {
            return player != target;
        }

        public override CardType Type => CardType.Basic;
    }
}
