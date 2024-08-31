using System.Threading.Tasks;
using Game.Core.Map;
using Game.Core.Units;

namespace Game.Skills
{
    public class MoveSkill : AbstractSkill
    {
        public override string Name => "移动";

        protected override async Task ExecuteAsync(SkillArgs args)
        {
            var owner = args.owner;
            var posList = Chess.Instance.GetMovablePositions(owner.Position, owner.MoveRange);
            var pos = await args.owner.Controller.SelectPosition(posList);
            owner.MoveTo(pos);
        }
    }
}