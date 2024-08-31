using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Abstract;
using Game.Core.Map;
using UnityEngine;

namespace Game.Controller
{
    public class PlayerController : IController
    {
        public async Task<Vector2Int> SelectPosition(List<Vector2Int> posList)
        {
            var tcs = new TaskCompletionSource<bool>();
            var res = Vector2Int.zero;

            using (new GridStatusSetter(GridStatus.Selectable, posList))
            {
                Chess.Instance.OnGridClicked += OnGridClicked;
                await tcs.Task;
                Chess.Instance.OnGridClicked -= OnGridClicked;
            }

            return res;

            void OnGridClicked(Vector2Int position)
            {
                res = position;
                tcs.SetResult(true);
            }
        }
    }
}