using Game.Abstract;

namespace Game.Charactor
{
    public class Liubei : AbstractCharactor
    {
        public Liubei() : base(1, "刘备", 4, 4, Sex.Male, Group.Shu, new())
        {
        }
    }
    public class Guanyu : AbstractCharactor
    {
        public Guanyu() : base(2, "关羽", 4, 4, Sex.Male, Group.Shu, new())
        {
        }
    }
    public class Zhangfei : AbstractCharactor
    {
        public Zhangfei() : base(3, "张飞", 4, 4, Sex.Male, Group.Shu, new())
        {
        }
    }
}