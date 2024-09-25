using System;
using Framework.Buttons;
using Game.Core.Turn;
using Game.Core.Units;
using Game.Abstract;
using Framework;

namespace Game.Mono.GameUI
{
    public class EndUseButton : FunctionButton
    {
        private Player player;

        private void Start()
        {
            EventManager.Subscribe<PlayerPhaseEnterArgs>(OnPlayerPhaseEnter);
            gameObject.SetActive(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventManager.Unsubscribe<PlayerPhaseEnterArgs>(OnPlayerPhaseEnter);
        }

        private void OnPlayerPhaseEnter(object sender, PlayerPhaseEnterArgs args)
        {
            if (sender is not Player player) return;
            if (!player.IsEnemy && args.phase == PhaseEnum.Use)
            {
                this.player = player;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        protected override void OnClick()
        {
            player.Controller.EndUse();
        }
    }
}