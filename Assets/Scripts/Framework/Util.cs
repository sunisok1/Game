using UnityEngine;

namespace Framework
{
    public static class Util
    {
        public static T InstantiateRectLocal<T>(T origin, Vector3 localPos, Quaternion quaternion, Transform parent) where T : Component
        {
            var t = Object.Instantiate(origin, parent);
            var rectTransform = t.transform as RectTransform;
            rectTransform.SetLocalPositionAndRotation(localPos, quaternion);
            return t;
        }
    }
}