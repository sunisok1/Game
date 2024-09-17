using Game.Core.Turn;
using UnityEngine;

namespace Game.Mono.GameUI
{
    public class GameUIMono : MonoBehaviour
    {
        [SerializeField] private SkillButtonManager skillButtonManager;

        private void Awake()
        {
            TurnSystem.Instance.OnPlayerTurnEnter += skillButtonManager.SetContent;
        }

        private void OnDestroy()
        {
            TurnSystem.Instance.OnPlayerTurnEnter -= skillButtonManager.SetContent;
        }
    }
}