using System;

namespace NoStackHack.Utilities
{
    public interface ICommand
    {
        void Execute(IGameObject gameObject);
    }
}
