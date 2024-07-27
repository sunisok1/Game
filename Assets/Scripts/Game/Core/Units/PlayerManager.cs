using System;
using UnityEngine;

namespace Game.Core.Units
{
    public static class PlayerManager
    {
        public static event Action<Player> OnPlayerCreate;

        public static Player CreatePlayer(Vector2 position)
        {
            var player = new Player(position);
            OnPlayerCreate?.Invoke(player);
            return player;
        }
    }
}