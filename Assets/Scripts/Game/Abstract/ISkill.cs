using System;
using System.Threading.Tasks;

namespace Game.Abstract
{
    public interface ISkill
    {
        public string Name { get; }

        public Task ExecuteAsync(EventArgs eventArgs);
    }
}