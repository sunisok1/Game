using System;
using UnityEngine;

namespace Game.Core.Utils
{
    public static class MathUtil
    {
        public static float ManhattanDistance(this Vector2 pos, Vector2 other)
        {
            return Math.Abs(pos.x - other.x) + Math.Abs(pos.y - other.y);
        }
    }
}