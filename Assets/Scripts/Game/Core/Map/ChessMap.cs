using System.Threading.Tasks;
using Framework.Singletons;
using UnityEngine;

namespace Game.Core.Map
{
    public class Chess : Singleton<Chess>
    {
        public async Task<Vector2> SelectPosition(Vector2 origin, int range)
        {
            throw new System.NotImplementedException();
        }
    }
}