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

    public class Chess : Singleton<Chess>
    {
        public event Action<Vector2, GridState> OnGridStateChange;

        public async Task<Vector2> SelectPosition(Vector2 origin, int range)
        {
            for (int i = -range; i <= range; i++)
            {
                for (int j = -range; j <= range; j++)
                {
                    if (Utils.MathUtil.ManhattanDistance(origin, origin + new Vector2(i, j)) > range)
                    {
                    }
                }
            }

            OnGridStateChange
        }
    }
}