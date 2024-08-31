using Game.Core.Units;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class SkillButtonManager : MonoBehaviour
    {
        [SerializeField] private SkillButton skillButtonPrefab;

        public void SetContent(Player player)
        {
            foreach (var skill in player.Skills)
            {
                var skillButton = Instantiate(skillButtonPrefab, transform);
                skillButton.SetText(skill.Name);
                skillButton.SetAction(() => player.TriggerSkill(skill));
            }
        }
    }
}