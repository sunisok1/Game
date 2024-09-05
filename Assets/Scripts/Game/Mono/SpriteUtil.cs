using System;
using System.IO;
using Framework.Singletons;
using Game.Abstract;
using Game.Core.Units;
using UnityEngine;

namespace Game.Mono
{
    public class SpriteUtil : MonoSingleton<SpriteUtil>
    {
        [SerializeField] private Sprite defaultMaleSkin;
        [SerializeField] private Sprite defaultFemaleSkin;
        [SerializeField] private Sprite defaultDoubleSkin;

        private const string skinPath = "Character";

        public Sprite GetClassicSkins(Player player)
        {
            var sprite = Resources.Load<Sprite>(Path.Combine(skinPath, $"{player.Id}00"));
            return sprite ?? GetDefaultSkin(player);
        }

        public Sprite GetDefaultSkin(Player player)
        {
            if (player.names.Length >= 2)
                return defaultDoubleSkin;
            return player.Sex == Sex.Femail ? defaultFemaleSkin : defaultMaleSkin;
        }
    }
}