using System;
using Game.Core.Turn;
using Game.Core.Units;
using Game.Mono;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Mono.GameUI
{
    public class MeAvatar : MonoBehaviour
    {
        [SerializeField] private Image avatarImage;
        private void Start()
        {
            TurnSystem.Instance.OnPlayerPhaseEnter += UpdateAvatar;
        }

        private void OnDestroy()
        {
            TurnSystem.Instance.OnPlayerPhaseEnter -= UpdateAvatar;
        }

        private void UpdateAvatar(Player player)
        {
            if (player.IsEnemy) return;
            avatarImage.sprite = SpriteUtil.Instance.GetClassicSkins(player);
        }
    }
}
