using System.Collections.Generic;
using UnityEngine;

namespace Game.Abstract
{
    public interface IPlayer
    {
        Vector2 Position { get; }
        IEnumerable<AbstractSkill> Skills { get; }
        void MoveTo(Vector2 pos);
        void AddSkill<T>() where T : AbstractSkill, new();
        bool RemoveSkill<T>() where T : AbstractSkill;
        bool HasSkill<T>() where T : AbstractSkill;
    }
}