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

        public static void MoveRectLocal(Transform transform, Vector3 localPos, Quaternion quaternion, Transform parent)
        {
            var rectTransform = transform as RectTransform;
            rectTransform.SetLocalPositionAndRotation(localPos, quaternion);
        }

        public static void DestoryAllChildren(this Transform transform)
        {

            foreach (Transform item in transform)
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}