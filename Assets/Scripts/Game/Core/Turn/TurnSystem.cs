using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Units;

namespace Game.Turn
{
    public class TurnManager
    {
        private readonly List<BaseUnit> playerUnit;
        private readonly List<BaseUnit> enemyUnit;

        private TurnManager([NotNull] List<BaseUnit> playerUnit, [NotNull] List<BaseUnit> enemyUnit)
        {
            this.playerUnit = playerUnit;
            this.enemyUnit = enemyUnit;
        }

        public void StartGame()
        {
        }
    }
}