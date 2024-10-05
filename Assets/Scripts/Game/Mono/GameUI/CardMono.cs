using System;
using Game.Abstract;
using Game.Core.Cards;
using Gmae.lib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;


namespace Game.Mono.GameUI
{
    public class CardMono : MonoBehaviour
    {
        private Card card;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Color red = Color.red;
        [SerializeField] private Color black = Color.black;
        [SerializeField] private Graphic[] buttonImages;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform bodyTransform;
        [SerializeField] private Color defaultColor = Color.gray;
        [SerializeField] private Color selectedColor = Color.red;
        [SerializeField] private Image shadowImage;
        public event Action<Card, bool> OnCardSelected;
        private const int translateY = 20;


        private bool selectable;
        private bool selected;
        public bool Selectable
        {
            get => selectable;
            set
            {
                selectable = value;
                button.interactable = value;
                SetUseable(value);
            }
        }
        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                bodyTransform.DOLocalMoveY(value ? translateY : 0, 0.5f);
                shadowImage.DOColor(value ? selectedColor : defaultColor, 0.5f);
                OnCardSelected(card, value);
            }
        }
        void SetUseable(bool useable)
        {
            button.interactable = useable;

            var imageColor = useable ? Color.white : Color.gray;
            foreach (var image in buttonImages)
            {
                image.DOColor(imageColor, 0.5f);
            }
        }

        private void Start()
        {
            button.onClick.AddListener(() => Selected ^= true);
        }

        public void Init(Card card)
        {
            this.card = card;
            infoText.text = $"{GetSuitStr(card.suit)} {GetPointStr(card.point)}";
            infoText.color = card.Color == CardColor.Red ? red : black;
            nameText.text = TextLoader.cardNameDict[card.name];
        }

        private object GetPointStr(int point)
        {
            return point switch
            {
                <= 0 => " ",
                <= 10 => point.ToString(),
                11 => "J",
                12 => "Q",
                13 => "K",
                _ => " ",
            };
        }

        private string GetSuitStr(Suit suit)
        {
            return suit switch
            {
                Suit.Spade => "♠︎",
                Suit.Heart => "♥︎",
                Suit.Club => "♣︎",
                Suit.Diamond => "♦︎",
                _ => " ",
            };
        }
    }
}