using System.Collections.Generic;

namespace Game.Abstract
{
    public abstract class AbstractCharactor
    {
        public readonly int id;


        public readonly string name;
        public readonly string[] names;

        public readonly int maxHp;
        public readonly int initHp;

        public readonly Sex sex;

        public readonly Group group;

        public readonly List<ISkill> skills = new();

        protected AbstractCharactor(int id, string name, int maxHp, int initHp, Sex sex, Group group, List<ISkill> skills)
        {
            this.id = id;
            this.name = name;
            names = new string[] { name };
            this.maxHp = maxHp;
            this.initHp = initHp;
            this.sex = sex;
            this.group = group;
            this.skills = skills;
        }
        protected AbstractCharactor(int id, string name, string[] names, int maxHp, int initHp, Sex sex, Group group, List<ISkill> skills)
        {
            this.id = id;
            this.name = name;
            this.names = names;
            this.maxHp = maxHp;
            this.initHp = initHp;
            this.sex = sex;
            this.group = group;
            this.skills = skills;
        }

    }
}