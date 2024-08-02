using System;
using Game.Core.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Mono
{
    public class GridMono : MonoBehaviour
    {
        [SerializeField] private Image pointerImage;
        [SerializeField] private Image selectableImage;
        [SerializeField] private Image unselectableImage;

        public void SetStatus(GridStatus status)
        {
            pointerImage.enabled = status == GridStatus.Pointer;
            selectableImage.enabled = status == GridStatus.Selectable;
            unselectableImage.enabled = status == GridStatus.Unselectable;
        }
    }
}