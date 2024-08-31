using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Abstract;
using UnityEngine;

namespace Game.Controller
{
    public class AiController : IController
    {
        public Task<Vector2Int> SelectPosition(List<Vector2Int> posList)
        {
            return Task.FromResult(posList.First());
        }
    }
}