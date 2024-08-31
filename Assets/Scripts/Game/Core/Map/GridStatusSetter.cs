using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Map
{

    public struct GridStatusSetter : IDisposable
    {
        private List<Vector2Int> _posList;

        public GridStatusSetter(GridStatus status, List<Vector2Int> posList)
        {
            _posList = posList;
            Chess.Instance.SetStatus(status, _posList);
        }

        public void Dispose()
        {
            Chess.Instance.SetStatus(GridStatus.None, _posList);
        }
    }
}
