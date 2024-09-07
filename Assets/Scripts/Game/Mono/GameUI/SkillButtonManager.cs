using System;
using Framework;
using Game.Core.Units;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class SkillButtonManager : MonoBehaviour
    {
        [SerializeField] private SkillButton skillButtonPrefab;

        private void Start()
        {
            EventManager.Subscribe<SkillUsedEventArgs>(OnSkillUsed);
        }

        private void OnSkillUsed(object sender, SkillUsedEventArgs e)
        {
            SetContent(sender as Player);
        }

        public void SetContent(Player player)
        {
            transform.DestoryAllChildren();

            foreach (var skill in player.CanUseSkills)
            {
                var skillButton = Instantiate(skillButtonPrefab, transform);
                skillButton.SetText(skill.Name);
                skillButton.SetAction(() => player.TriggerSkill(skill));
            }
        }
    }
}