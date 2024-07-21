using UnityEngine;

namespace Framework.Singletons
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }


        private void Awake()
        {
            Instance = this as T;
        }
    }
}