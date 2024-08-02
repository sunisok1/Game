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

        [SerializeField] private PlayerMono playerPrefab;
        [SerializeField] private GridMono gridPrefab;
        [SerializeField] private Transform playerContent;
        [SerializeField] private Transform gridContent;

        private void Start()
        {
            (transform as RectTransform).sizeDelta = new(width * gridW, height * gridH);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grids[i, j] = Util.InstantiateRectLocal(gridPrefab, GetGridPos(i, j), Quaternion.identity, gridContent);
                }
            }

            PlayerManager.OnPlayerCreate += OnPlayerCreate;
            EventManager.Subscribe<GridStateChangeArgs>(OnGridStateChange);
        }

        private void OnDestroy()
        {
            PlayerManager.OnPlayerCreate -= OnPlayerCreate;
            EventManager.Unsubscribe<GridStateChangeArgs>(OnGridStateChange);
        }

        private static Vector3 GetGridPos(int i, int j)
        {
            return new((i + 0.5f) * gridW, (j + 0.5f) * gridH);
        }


        private void OnGridStateChange(object sender, GridStateChangeArgs args)
        {
            grids[args.Position.x, args.Position.y].SetStatus(args.Status);
        }


        private void OnPlayerCreate(Player obj)
        {
            Util.InstantiateRectLocal(playerPrefab, GetGridPos(obj.Position.x, obj.Position.y), Quaternion.identity, playerContent);
        }
    }
}