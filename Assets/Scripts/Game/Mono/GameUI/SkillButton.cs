using System;
using Framework.Buttons;
using Game.Abstract;
using TMPro;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class SkillButton : FunctionButton
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        private Action action;

        protected override void OnClick()
        {
            if (action is null) throw new Exception("SkillButton Action is null");
            action();
        }

        public void SetText(string text)
        {
            textMeshProUGUI.text = text;
        }

        public void SetAction(Action action)
        {
            this.action = action;
        }
    }
}