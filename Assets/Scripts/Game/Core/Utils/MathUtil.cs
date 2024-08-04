using System;
using UnityEngine;

namespace Game.Core.Utils
{
    public static class MathUtil
    {
        public static int ManhattanDistance(this Vector2Int pos, Vector2Int other)
        {
            var d = pos - other;
            return d.x.Abs() + d.y.Abs();
        }

        public static int Abs(this int i)
        {
            return i > 0 ? i : -i;
        }
    }
}