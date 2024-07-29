using System.Threading.Tasks;
using UnityEngine;

namespace Game.Abstract
{
    public interface IPlayer
    {
        Vector2 Position { get; }
        void MoveTo(Vector2 pos);
        void AddSkill<T>() where T : AbstractSkill, new();
        bool RemoveSkill<T>() where T : AbstractSkill;
        bool HasSkill<T>() where T : AbstractSkill;
    }
}