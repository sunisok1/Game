using System;
using Game.Core.Map;
using Game.Core.Units;
using UnityEngine;

namespace Game.Mono
{
    public class PlayerMono : MonoBehaviour
    {
        private Player player;
        public void Init(Player player)
        {
            this.player = player;
            player.OnMove += OnMove;
        }

        private void OnDestroy()
        {
            player.OnMove -= OnMove;
        }

        private void OnMove(Vector2Int pos)
        {
            ChessMono.Instance.MoveObject(pos, transform);
        }
    }
}
