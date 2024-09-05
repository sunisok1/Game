using System;
using System.Threading.Tasks;
using Game.Abstract;
using Game.Core.Units;

namespace Game.Skills
{
    public abstract class SkillBase : ISkill
    {
        public abstract string Name { get; }

        async Task ISkill.ExecuteAsync(EventArgs eventArgs)
        {
            if (eventArgs is SkillArgs args)
            {
                await ExecuteAsync(args);
            }
        }

        protected abstract Task ExecuteAsync(SkillArgs args);
    }
}