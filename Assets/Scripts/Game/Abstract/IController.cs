using System.Threading.Tasks;
using UnityEngine;

namespace Game.Abstract
{
    public interface IController
    {
        Task<Vector2Int> SelectPosition();
    }
}