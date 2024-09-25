namespace Game.Abstract
{
    public struct IntRange
    {
        public readonly int max;
        public readonly int min;
        public IntRange(int min, int max)
        {
            this.max = max;
            this.min = min;
        }

        public bool InRange(int num)
        {
            return num >= min && num <= max;
        }
    }
}