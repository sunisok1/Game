using System.Threading.Tasks;
using Game.Abstract;
using Game.Core.Map;
using UnityEngine;

namespace Game.Skills
{
    public class MoveSkill : AbstractSkill
    {
        public override async Task ExecuteAsync(IPlayer player)
        {
            Vector2 pos = await Chess.Instance.SelectPosition(player.Position, 2);
            player.MoveTo(pos);
        }
    }
}