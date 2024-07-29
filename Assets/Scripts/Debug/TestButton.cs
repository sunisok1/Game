using Framework.Buttons;
using Game.Core.Units;

namespace Debug
{
    public class TestButton : FunctionButton
    {
        protected override void OnClick()
        {
            Player player = new(new(1, 2));
        }
    }
}