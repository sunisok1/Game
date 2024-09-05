using System;
using Game.Core.Map;
using Game.Core.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Mono
{
    public class PlayerMono : MonoBehaviour
    {
        private Player player;
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI nameText;
        public void Init(Player player)
        {
            this.player = player;
            BindEvents(player);
            nameText.text = player.name;
            avatarImage.sprite = SpriteUtil.Instance.GetClassicSkins(player);
        }


        private void BindEvents(Player player)
        {
            player.OnMove += OnMove;
        }


        private void UnbindEvents()
        {
            player.OnMove -= OnMove;
        }

        private void OnMove(Vector2Int pos)
        {
            ChessMono.Instance.MoveObject(pos, transform);
        }

        private void OnDestroy()
        {
            UnbindEvents();
        }
    }
}
