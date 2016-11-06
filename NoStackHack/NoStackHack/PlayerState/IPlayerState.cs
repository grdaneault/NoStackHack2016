using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;

namespace NoStackHack.PlayerState
{
    public interface IPlayerState : IState, IJumper, IMovable
    {
    }
}
