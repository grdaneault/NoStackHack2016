﻿using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;

namespace NoStackHack
{
    public class Player : IJumper, IMovable
    {
        public Vector2 Position { get; set; } = new Vector2(500, 500);
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Acceleration { get; set; } = Vector2.Zero;

        public Box Box { get { return new Box(Position, Vector2.One * 50); } }
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
            Acceleration += new Vector2(direction.X, -direction.Y);
            
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
            Velocity -= Velocity * .1f;
            Position += Velocity;

            Acceleration = Vector2.Zero;
        }
        //public Vector2 Position
        //{
        //    get { return new Vector2(_x, _y); }
        //}
    }
}
