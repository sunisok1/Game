﻿using UnityEngine;

namespace Framework.Singletons
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }


        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}