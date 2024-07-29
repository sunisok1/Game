using System.Threading.Tasks;

namespace Game.Abstract
{
    public abstract class AbstractSkill
    {
        public abstract string Name { get; }

        public abstract Task ExecuteAsync(IPlayer player);
    }
}