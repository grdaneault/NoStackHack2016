using Microsoft.Xna.Framework;

namespace NoStackHack.ControlInput.Commands
{
    class MoveCommand : IStickCommand
    {
        public Vector2 Direction { get; set; }

        public void Execute(IGameObject gameObject)
        {
            var movable = gameObject as IMovable;
            if (movable == null) { return; }
            movable.Move(Direction);
        }
    }
}
