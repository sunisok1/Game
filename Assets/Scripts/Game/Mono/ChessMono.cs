using System;
using Framework;
using Framework.Singletons;
using Game.Core;
using Game.Core.Map;
using Game.Core.Turn;
using Game.Core.Units;
using UnityEngine;

namespace Game.Mono
{
    public class ChessMono : MonoSingleton<ChessMono>
    {
        private const float gridH = 144;
        private const float gridW = 144;
        private const int width = Chess.width;
        private const int height = Chess.height;
        private readonly GridMono[,] grids = new GridMono[width, height];
        public event Action<Vector2Int> OnGridClicked;
        [SerializeField] private PlayerMono playerPrefab;
        [SerializeField] private GridMono gridPrefab;
        [SerializeField] private Transform playerContent;
        [SerializeField] private Transform gridContent;

        private void Start()
        {
            (transform as RectTransform).sizeDelta = new(width * gridW, height * gridH);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grids[x, y] = Util.InstantiateRectLocal(gridPrefab, GetGridPos(x, y), Quaternion.identity, gridContent);
                    grids[x, y].Init(new(x, y));
                }
            }

            PlayerManager.OnPlayerCreate += OnPlayerCreate;
        }

        private void OnDestroy()
        {
            PlayerManager.OnPlayerCreate -= OnPlayerCreate;
        }

        private static Vector3 GetGridPos(int i, int j)
        {
            return new((i + 0.5f) * gridW, (j + 0.5f) * gridH);
        }

        public void MoveObject(Vector2Int pos, Transform transform)
        {
            Util.MoveRectLocal(transform, GetGridPos(pos.x, pos.y), Quaternion.identity, playerContent);
        }

        public void SetStatus(Vector2Int position, GridStatus status)
        {
            grids[position.x, position.y].SetStatus(status);
        }

        public void ClickGrid(Vector2Int position)
        {
            OnGridClicked?.Invoke(position);
        }

        private void OnPlayerCreate(Player player)
        {
            var playerMono = Util.InstantiateRectLocal(playerPrefab, GetGridPos(player.Position.x, player.Position.y), Quaternion.identity, playerContent);
            playerMono.Init(player);
        }
    }
}