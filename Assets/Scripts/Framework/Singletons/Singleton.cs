using System;

namespace Framework.Singletons
{
    public abstract class Singleton<T> : IDisposable where T : class, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
            private set => instance = value;
        }
        public virtual void Dispose()
        {
            Instance = default;
        }
    }
}