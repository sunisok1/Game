namespace Framework.Singletons
{
    public class Singleton<T> where T : new()
    {
        public readonly T  Instance = new();

        private Singleton()
        {
        }
    }
}