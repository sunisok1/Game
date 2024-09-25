using System;
using Game.Abstract;
using Game.Core.Cards;
using Gmae.lib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Mono.GameUI
{
    public class CardMono : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Color red = Color.red;
        [SerializeField] private Color black = Color.black;
        [SerializeField] private Graphic[] buttonImages;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform bodyTransform;
        private const int translateY = -20;
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
            }
        }


        private void Start()
        {
            button.onClick.AddListener(SwitchState);
        }

        private void SwitchState()
        {
            Selected = !Selected;
        }

        public void Init(Card card)
        {
            infoText.text = $"{GetSuitStr(card.suit)} {GetPointStr(card.point)}";
            infoText.color = card.Color == CardColor.Red ? red : black;
            nameText.text = TextLoader.cardNameDict[card.name];
        }

        internal void SetUseable(bool useable)
        {
            button.interactable = useable;

            var imageColor = useable ? Color.white : Color.gray;
            foreach (var item in buttonImages)
            {
                item.color = imageColor;
            }
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