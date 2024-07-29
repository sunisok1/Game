using Framework;
using UnityEngine;

namespace Game.Core.Map
{
    public partial class Chess
    {
        private class Grid
        {
            public Vector2Int Position { get; }

            public Grid(Vector2Int position)
            {
                Position = position;
            }

            public void SetGridState(GridState state)
            {
                EventManager.InvokeEvent(this, new GridStateChangeArgs());
            }
        }
    }
}