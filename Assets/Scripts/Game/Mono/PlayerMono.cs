using System;
using Codice.Client.Commands;
using Game.Core.Map;
using Game.Core.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Mono
{
    public class PlayerMono : MonoBehaviour
    {
        private Player player;
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image shadow;
        [SerializeField] private Color SelectableColor = Color.blue;
        [SerializeField] private Color UnselectableColor = Color.gray;
        [SerializeField] private Color SelectedColor = Color.red;
        [SerializeField] private Button button;
        public static event Action<Player, bool> OnPlayerSelected;

        private bool selectable;
        private bool selected;
        public bool Selectable
        {
            get => selectable;
            set
            {
                if (selectable == value) return;
                selectable = value;
                button.interactable = value;
                shadow.color = value ? SelectableColor : UnselectableColor;
            }
        }
        public bool Selected
        {
            get => selected;
            set
            {
                if (!selectable) return;
                if (selected == value) return;
                selected = value;
                OnPlayerSelected?.Invoke(player, value);
                shadow.color = value ? SelectedColor : SelectableColor;
            }
        }

        public void Init(Player player)
        {
            this.player = player;
            BindEvents(player);
            nameText.text = player.name;
            avatarImage.sprite = SpriteUtil.Instance.GetClassicSkins(player);
            button.onClick.AddListener(() => Selected ^= true);
        }

        public void ForceSelect()
        {
            Selectable = false;
            Selected = true;
        }

        private void BindEvents(Player player)
        {
            player.OnMove += OnMove;
        }


        private void UnbindEvents()
        {
            player.OnMove -= OnMove;
        }

        private void OnMove(Vector2Int pos)
        {
            ChessMono.Instance.MoveObject(pos, transform);
        }

        private void OnDestroy()
        {
            UnbindEvents();
        }
    }
}
