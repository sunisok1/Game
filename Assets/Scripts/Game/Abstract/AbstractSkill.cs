using System.Threading.Tasks;

namespace Game.Abstract
{
    public abstract class AbstractSkill
    {
        protected IPlayer owner;

        protected AbstractSkill(IPlayer owner)
        {
            this.owner = owner;
        }

        public abstract string Name { get; }

        public abstract Task ExecuteAsync();
    }
}