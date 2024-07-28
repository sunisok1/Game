using Framework.Singletons;
using Game.Core.Units;
using UnityEngine;

namespace Game.Core.Mono
{
    public class ChessMono : MonoSingleton<ChessMono>
    {
        [SerializeField] private PlayerMono playerPrefab;
        private const float gridH = 144;
        private const float gridW = 144;

        protected override void Awake()
        {
            base.Awake();
            PlayerManager.OnPlayerCreate += OnPlayerCreate;
        }

        private void OnDestroy()
        {
            PlayerManager.OnPlayerCreate -= OnPlayerCreate;
        }

        private void OnPlayerCreate(Player obj)
        {
            Vector2 pos = obj.Position;
            var player = Instantiate(playerPrefab, transform);
            var playerTransform = player.transform as RectTransform;
            playerTransform.SetLocalPositionAndRotation(new(pos.x * gridW, pos.y * gridH), Quaternion.identity);
        }
    }
}