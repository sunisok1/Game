using Game.Core.Map;
using Game.Core.Turn;
using UnityEngine;

namespace Game.Mono
{
    public class Disposer : MonoBehaviour
    {
        private void OnDestroy()
        {
            TurnSystem.Instance.Dispose();
            Chess.Instance.Dispose();
        }
    }
}