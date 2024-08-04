using Game.Abstract;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class SkillButtonManager : MonoBehaviour
    {
        [SerializeField] private SkillButton skillButtonPrefab;
        public void SetContent(IPlayer player)
        {
            foreach (var skill in player.Skills)
            {
                var button = Instantiate(skillButtonPrefab, transform);
                button.Init(skill);
            }
        }
    }
}