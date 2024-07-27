namespace Game.Core.Units
{
    public interface ITurnStrategy
    {
        void StartTurn(BaseUnit unit);
        void EndTurn(BaseUnit unit);
    }
}