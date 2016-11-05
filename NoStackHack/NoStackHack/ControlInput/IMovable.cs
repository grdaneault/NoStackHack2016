using Microsoft.Xna.Framework;

namespace NoStackHack.ControlInput
{
    public interface IMovable : IGameObject
    {
        void Move(Vector2 translation);
    }
}
