using System;
using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;

namespace NoStackHack
{
    public class Player : IJumper, IMovable, IResetable
    {
        public Vector2 Position { get; set; } = new Vector2(500, 500);
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Acceleration { get; set; } = Vector2.Zero;

        public Box Box { get { return new Box(Position, new Vector2(50, 100)); } }
        //private float _x, _y;

        public Player() { }

        public Player(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public void Move(Vector2 direction)
        {
            //_x += direction.X;
            //_y -= direction.Y;
            Acceleration += new Vector2(direction.X, 0);
            
        }

        public void Jump()
        {
            //_y += 10;
            Acceleration -= Vector2.UnitY * 10;
        }

        public PlayerIndex PlayerIndex
        {
            get; private set;
        }

        public void Update()
        {

            Velocity += Acceleration;

            if (Velocity.Length() > 50)
            {
                Velocity = Velocity.Normal() * 50;
            }

            Velocity -= new Vector2(Velocity.X * .1f, Velocity.Y * .05f);
            Position += Velocity;

            Acceleration = Vector2.Zero;
        }

        public void ResetPosition()
        {
            Position = Vector2.Zero;
            Acceleration = Vector2.Zero;
        }
        //public Vector2 Position
        //{
        //    get { return new Vector2(_x, _y); }
        //}
    }
}
