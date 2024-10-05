using System.Collections.Generic;
using Framework.Buttons;
using Game.Charactor;
using Game.Core;
using Game.Core.Cards;
using Game.Core.Turn;
using Game.Core.Units;
using Game.Skills;

namespace Game.BeforeBattle
{
    public class Prepare : FunctionButton
    {
        protected override void OnClick()
        {
            CardPile.Instance.Init(Cards.Standard.cards);
            var players = new List<Player>()
            {
                new(new(1, 3),new Liubei()),
                new(new(1, 2),new Guanyu()),
                new(new(1, 1),new Zhangfei()),
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