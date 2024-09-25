using System;
using Game.Core.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Mono
{
    public class GridMono : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite defaultImage;
        [SerializeField] private Sprite pointerImage;
        [SerializeField] private Sprite selectableImage;
        [SerializeField] private Sprite unselectableImage;

        [SerializeField] private Button button;
        private Vector2Int pos;
        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Init(Vector2Int pos)
        {
            this.pos = pos;

        }
        private void OnClick()
        {
            ChessMono.Instance.ClickGrid(pos);
        }

        public void SetStatus(GridStatus status)
        {
            button.enabled = status == GridStatus.Selectable;
            image.sprite = status switch
            {
                GridStatus.None => defaultImage,
                GridStatus.Pointer => pointerImage,
                GridStatus.Selectable => selectableImage,
                GridStatus.Unselectable => unselectableImage,
                _ => throw new NotImplementedException(),
            };
        }
    }
}