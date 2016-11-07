using Microsoft.Xna.Framework;

namespace NoStackHack.ControlInput.Commands
{
    class RotateCommand : IStickCommand
    {
        public Vector2 Direction { get; set; }

        public void Execute(IGameObject gameObject)
        {
            var rotatable = gameObject as IRotatable;
            if (rotatable == null) { return; }
            rotatable.Rotate(Direction);
        }
    }
}
