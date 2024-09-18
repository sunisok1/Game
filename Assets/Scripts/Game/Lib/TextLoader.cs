
using System.Collections.Generic;
using UnityEngine;

namespace Gmae.lib
{
    public static class TextLoader
    {
        public static IReadOnlyDictionary<string, string> cardNameDict;

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            cardNameDict = new CardNameDict();
        }
    }

    internal class CardNameDict : Dictionary<string, string>
    {
        public CardNameDict()
        {
            Add("sha", "杀");
            Add("shan", "闪");
            Add("tao", "桃");
        }
    }
}