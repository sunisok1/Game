using System;
using Game.Abstract;
using Game.Core.Cards;

namespace Game.Core.Units
{
    public class UseCardArgs : EventArgs
    {
        public Player player;
        public Func<Card, Player, EventArgs, bool> filterCard;
        public IntRange selectCard;
        public Func<Card, Player, Player, bool> filterTarget;
        public Func<Card, Player, IntRange> selectTarget;
    }
}