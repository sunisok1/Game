using System;

namespace Game.Core.Units

{
    public class SkillArgs : EventArgs
    {
        public SkillArgs(Player owner)
        {
            this.owner = owner;
        }

        public Player owner { get; }
    }
}