using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
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
    public class GridStateChangeArgs : EventArgs
    {
        public Vector2Int Position { get; }
        public GridStatus Status { get; }

        public GridStateChangeArgs(Vector2Int position, GridStatus status)
        {
            Position = position;
            Status = status;
        }
    }

    public partial class Chess : Singleton<Chess>
    {
        private class Grid
        {
            public Vector2Int Position { get; }

            public Grid(Vector2Int position)
            {
                Position = position;
            }
        }


        public const int width = 5;
        public const int height = 5;
        private readonly Grid[,] grids = new Grid[width, height];

        public event Action<GridStateChangeArgs> OnGridStateChange;

        public event Action<Vector2Int> OnGridClicked;

        public Chess()
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    grids[i, j] = new(new(i, j));
                }
            }
        }

        public List<Vector2Int> GetMovablePositions(Vector2Int origin, int range)
        {
            var list = new List<Vector2Int>();
            var minX = Mathf.Max(0, origin.x - range);
            var maxX = Mathf.Min(width - 1, origin.x + range);
            var minY = Mathf.Max(0, origin.y - range);
            var maxY = Mathf.Min(height - 1, origin.y + range);

            for (var x = minX; x <= maxX; x++)
            {
                var deltaX = Mathf.Abs(x - origin.x);
                var remainingRange = range - deltaX;
                var startY = Mathf.Max(minY, origin.y - remainingRange);
                var endY = Mathf.Min(maxY, origin.y + remainingRange);

                for (var y = startY; y <= endY; y++)
                {
                    list.Add(new Vector2Int(x, y));
                }
            }

            return list;
        }

        public void SetStatus(GridStatus status, IEnumerable<Vector2Int> positions)
        {
            foreach (var position in positions)
            {
                OnGridStateChange?.Invoke(new GridStateChangeArgs(position, status));
            }
        }

        public void ClickGrid(Vector2Int position)
        {
            OnGridClicked?.Invoke(position);
        }


        public bool IsWithinGridBounds(int x, int y)
        {
            return x is >= 0 and < width && y is >= 0 and < height;
        }
    }
}