using System;
using Game.Core.Units;
using UnityEngine;

namespace Game.Controller
{
    public class ControllerAssigner : MonoBehaviour
    {
        private void Start()
        {
            PlayerManager.OnPlayerCreate += AssignController;
        }

        private void OnDestroy()
        {
            PlayerManager.OnPlayerCreate -= AssignController;
        }

        private void AssignController(Player player)
        {
            if (player.IsEnemy)
            {
                player.Controller = new AiController();
            }
            else
            {
                player.Controller = new PlayerController();
            }
        }
    }
}