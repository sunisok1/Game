using System;
using System.Threading.Tasks;
using Game.Abstract;
using Game.Core.Units;

namespace Game.Skills
{
    [Flags]
    public enum SkillType
    {
        EquipSkill,//装备技能
        ForceSkill,//锁定技
        ZhuSkill,//主公技
        LimitedSkill,//限定技
    }

    public abstract class SkillBase : ISkill
    {
        public abstract string Name { get; }

        public int Usable { get; protected set; } = int.MaxValue;
        protected SkillType SkillType { private get; set; }
        public bool IsSkill(SkillType skillType)
        {
            return (skillType & SkillType) == skillType;
        }
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