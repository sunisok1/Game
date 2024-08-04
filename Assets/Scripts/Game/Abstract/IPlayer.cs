using System.Collections.Generic;
using UnityEngine;

namespace Game.Abstract
{
    public interface IPlayer
    {
        Vector2Int Position { get; }
        IEnumerable<AbstractSkill> Skills { get; }
        IController Controller { get; set; }
        void MoveTo(Vector2Int pos);
        void AddSkill<T>() where T : AbstractSkill, new();
        bool RemoveSkill<T>() where T : AbstractSkill;
        bool HasSkill<T>() where T : AbstractSkill;
    }
}