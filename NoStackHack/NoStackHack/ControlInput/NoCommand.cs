using System;
using NoStackHack.Utilities;

namespace NoStackHack.ControlInput
{
    public class NoCommand : ICommand
    {
        public void Execute(IGameObject gameObject)
        {
            // do nothing
            return;
        }
    }
}
