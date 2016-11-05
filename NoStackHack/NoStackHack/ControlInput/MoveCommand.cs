using Microsoft.Xna.Framework;
using NoStackHack.Utilities;

namespace NoStackHack.ControlInput
{
    public class MoveCommand : ICommand
    {
        public void Execute(IGameObject gameObject)
        {
            var movable = gameObject as IMovable;
            if (movable == null) { return; }
            movable.Move(Direction * 5);
        }

        public Vector2 Direction { get; set; }
    }
}
