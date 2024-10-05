using System;
using Framework.Singletons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Mono.GameUI
{
    public class PlayerButtonManager : MonoSingleton<PlayerButtonManager>
    {
        [SerializeField] private EndUseButton endUseButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        public void ShowConfirmButton(UnityAction confirmAction, bool interactable = false)
        {
            confirmButton.gameObject.SetActive(true);
            SetConfirmInteractable(interactable);
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(confirmAction);
        }

        public void ShowCancelButton(UnityAction cancelAction)
        {
            cancelButton.gameObject.SetActive(true);
            cancelButton.interactable = true;
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(cancelAction);
        }

        public void SetConfirmInteractable(bool interactable)
        {
            confirmButton.interactable = interactable;
        }

        public void HideOperateButtons()
        {
            confirmButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }
    }
}