using Microsoft.Xna.Framework;
using NoStackHack.Utilities;

namespace NoStackHack.ControlInput
{
    interface IStickCommand : ICommand
    {
        Vector2 Direction { get; set; }
    }
}
