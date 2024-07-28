using System.Collections.Generic;
using Framework;
using Game.Core.Units;

namespace Game.Core
{
    public class GameEntry : Entry
    {
        private async void Start()
        {
            var players = new List<Player>()
            {
                new(new(1, 2)),
                new(new(1, 4)),
                new(new(2, 1)),
            };
            Status status = new()
            {
                players = players
            };

            var chess = new Chess(status);
            await chess.StartAsync();
        }
    }
}