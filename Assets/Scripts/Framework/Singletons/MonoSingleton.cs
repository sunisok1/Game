using UnityEngine;

namespace Framework.Singletons
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }


        protected virtual void Awake()
        {
            if (Instance)
            {
                throw new System.Exception($"Instance of {typeof(T)} already exists.");
            }
            Instance = this as T;
        }
    }
}