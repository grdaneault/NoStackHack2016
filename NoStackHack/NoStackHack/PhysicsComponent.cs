using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack
{
    public class PhysicsComponent
    {

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public PhysicsComponent()
        {

        }

        public void Update(GameTime time)
        {
            Acceleration += Vector2.UnitY * 2f; // its gravity!

            Velocity += Acceleration;

            if (Velocity.Length() > 50)
            {
                Velocity = Velocity.Normal() * 50;
            }

            Velocity -= new Vector2(Velocity.X * .1f, Velocity.Y * .05f);
            Position += Velocity;

            Acceleration = Vector2.Zero;
        }

    }
}
