using System;
using UnityEngine;

namespace Game.Core.Utils
{
    public static class MathUtil
    {
        public static int ManhattanDistance(this Vector2Int pos, Vector2Int other)
        {
            return Math.Abs(pos.x - other.x) + Math.Abs(pos.y - other.y);
        }

        public static int Abs(this int i)
        {
            return i > 0 ? i : -i;
        }
    }
}