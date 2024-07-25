using System;
using Framework.Singletons;
using Game.Units;
using UnityEngine;

namespace Game.Mono
{
    public class ChessMono : MonoSingleton<ChessMono>
    {
        [SerializeField] private GameObject playerPrefab;

        private void Start()
        {
            PlayerManager.OnPlayerCreate += OnPlayerCreate;
        }

        private void OnDestroy()
        {
            PlayerManager.OnPlayerCreate -= OnPlayerCreate;
        }

        private void OnPlayerCreate(Player obj)
        {
            Instantiate(playerPrefab, obj.Position, Quaternion.identity, transform);
        }
    }
}