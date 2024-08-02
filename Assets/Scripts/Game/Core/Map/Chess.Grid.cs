using System;
using Framework;
using UnityEngine;

namespace Game.Core.Map
{
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

    public partial class Chess
    {
        private class Grid
        {
            public Vector2Int Position { get; }

            public Grid(Vector2Int position)
            {
                Position = position;
            }

            public void SetGridState(GridStatus status)
            {
                EventManager.InvokeEvent(this, new GridStateChangeArgs(Position, status));
            }
        }
    }
}