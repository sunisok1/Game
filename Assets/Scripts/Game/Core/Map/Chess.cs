using System;
using System.Collections.Generic;
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

        public List<Vector2Int> GetPositionList(Vector2Int origin, int range, Func<Vector2Int, bool> filter)
        {
            var list = new List<Vector2Int>();
            Vector2Int pos = origin - new Vector2Int(range, range);
            var maxX = Mathf.Max(grids.GetLength(0), origin.x + range);
            var maxY = Mathf.Max(grids.GetLength(1), origin.y + range);
            for (pos.x = Mathf.Max(0, pos.x); pos.x <= maxX; pos.x++)
            for (pos.y = Mathf.Max(0, pos.y); pos.y <= maxY; pos.y++)
            {
                if (filter == null)
                {
                    list.Add(pos);
                }
                else if (filter(pos))
                {
                    list.Add(pos);
                }
            }

            return list;
        }

        public bool IsWithinGridBounds(int x, int y)
        {
            return x >= 0 && x < grids.GetLength(0) && y >= 0 && y < grids.GetLength(1);
        }
    }
}