using Framework.Buttons;
using Game.Core.Units;

namespace Debug
{
    public class TestButton : FunctionButton
    {
        protected override void OnClick()
        {
            PlayerManager.CreatePlayer(new(1, 1));
        }
    }
}