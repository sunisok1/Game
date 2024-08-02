using System;
using System.Threading.Tasks;
using Framework.Singletons;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Map
{
    public enum GridStatus
    {
        None,
        Pointer,
        Selectable,
        Unselectable
    }

    public partial class Chess : Singleton<Chess>
    {
        public const int width = 5;
        public const int height = 5;
        private readonly Grid[,] grids = new Grid[width, height];

        public Chess()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grids[i, j] = new(new(i, j));
                }
            }
        }

        public async Task<Vector2Int> SelectPosition(Vector2Int origin, int range)
        {
            for (var i = -range; i <= range; i++)
            {
                for (var j = -range; j <= range; j++)
                {
                    var x = origin.x + i;
                    var y = origin.y + j;
                    if (!IsWithinGridBounds(x, y))
                        continue;
                    if (i.Abs() + j.Abs() <= range)
                    {
                        grids[x, y].SetGridState(GridStatus.Pointer);
                    }
                }
            }

            return Vector2Int.zero;
        }

        public bool IsWithinGridBounds(int x, int y)
        {
            return x >= 0 && x < grids.GetLength(0) && y >= 0 && y < grids.GetLength(1);
        }
    }
}