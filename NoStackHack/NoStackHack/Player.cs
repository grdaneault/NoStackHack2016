using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;

namespace NoStackHack
{
    public class Player : IJumper, IMovable
    {
        private float _x, _y;

        public Player() { }

        public Player(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public void Move(Vector2 direction)
        {
            _x += direction.X;
            _y -= direction.Y;
        }

        public void Jump()
        {
            _y += 10;
        }

        public PlayerIndex PlayerIndex
        {
            get; private set;
        }

        public Vector2 Position
        {
            get { return new Vector2(_x, _y); }
        }
    }
}
