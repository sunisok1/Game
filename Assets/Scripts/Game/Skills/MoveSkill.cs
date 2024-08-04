using System.Threading.Tasks;
using Framework;
using Game.Abstract;
using Game.Core.Map;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Skills
{
    public class MoveSkill : AbstractSkill
    {
        public override string Name => "移动";

        public override async Task ExecuteAsync()
        {
            var posList = Chess.Instance.GetPositionList(owner.Position, 2, i => owner.Position.ManhattanDistance(i) <= 2);
            var pos = await owner.Controller.SelectPosition();
            owner.MoveTo(pos);
        }

        public MoveSkill(IPlayer owner) : base(owner)
        {
        }
    }
}