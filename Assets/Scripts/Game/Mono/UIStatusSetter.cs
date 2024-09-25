using System;
using System.Collections.Generic;
using Game.Core.Map;
using UnityEngine;

namespace Game.Mono
{

    public class GridStatusSetter : IDisposable
    {
        private List<Vector2Int> _posList;

        public GridStatusSetter(GridStatus status, List<Vector2Int> posList)
        {
            foreach (var position in posList)
            {
                ChessMono.Instance.SetStatus(position, status);
            }
            _posList = posList;
        }

        public void Dispose()
        {
            foreach (var position in _posList)
            {
                ChessMono.Instance.SetStatus(position, GridStatus.None);
            }
        }
    }
}
