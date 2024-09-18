using System;
using Game.Abstract;
using Gmae.lib;
using TMPro;
using UnityEngine;
namespace Game.Mono.GameUI
{
    public class CardMono : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Color red = Color.red;
        [SerializeField] private Color black = Color.black;
        public void Init(AbstractCard card)
        {
            infoText.text = $"{GetSuitStr(card.suit)} {GetPointStr(card.point)}";
            infoText.color = card.color == CardColor.Red ? red : black;
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