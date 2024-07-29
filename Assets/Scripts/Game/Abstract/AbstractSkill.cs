using System.Threading.Tasks;

namespace Game.Abstract
{
    public abstract class AbstractSkill
    {
        public AbstractSkill()
        {
        }

        public abstract Task ExecuteAsync(IPlayer player);
    }
}