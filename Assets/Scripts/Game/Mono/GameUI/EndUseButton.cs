using Framework.Buttons;
using Game.Core.Turn;

namespace Game.Mono.GameUI
{
    public class EndUseButton : FunctionButton
    {
        protected override void OnClick()
        {
            TurnSystem.Instance.CurPlayer.EndUse();
        }
    }
}