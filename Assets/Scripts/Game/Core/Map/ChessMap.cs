using System;
using System.Threading.Tasks;
using Framework.Singletons;
using UnityEngine;

namespace Game.Core.Map
{
    public enum GridState
    {
        None,
        Pointer,
        Selectable
    }

    public class GridStateChangeArgs : EventArgs
    {
    }

    public partial class Chess : Singleton<Chess>
    {
        public int width { get; private set; }
        public int height { get; private set; }
        private Grid[,] grids;

        public void Init(int width, int height)
        {
            this.width = width;
            this.height = height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grids[i, j] = new(new(i, j));
                }
            }
        }

        public event Action<Vector2Int, GridState> OnGridStateChange;

        public async Task<Vector2Int> SelectPosition(Vector2Int origin, int range)
        {
            for (int i = -range; i <= range; i++)
            {
                for (int j = -range; j <= range; j++)
                {
                    if (Utils.MathUtil.ManhattanDistance(origin, grids[i, j].Position) <= range)
                    {
                        grids[i, j].SetGridState(GridState.Pointer);
                    }
                }
            }

            return Vector2Int.zero;
        }
    }
}