using System.Collections.Generic;
using Framework;
using Game.Core;
using Game.Core.Turn;
using Game.Core.Units;
using Game.Skills;

namespace Game.Game
{
    public class GameEntry : Entry
    {
        private void Start()
        {
            var players = new List<Player>()
            {
                new(new(1, 2))
                {
                    name = "guanyu"
                },
                new(new(1, 4))
                {
                    name = "zhangfei"
                },
                new(new(2, 1))
                {
                    name = "liubei"
                },
            };
            foreach (var player in players)
            {
                player.AddSkill<MoveSkill>();
            }

            Status status = new()
            {
                players = players
            };

            TurnSystem.Instance.StartAsync(status);
        }
    }
}