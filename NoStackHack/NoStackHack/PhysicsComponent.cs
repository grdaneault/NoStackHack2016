using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack
{
    public abstract class PhysicsComponent<TValue> where TValue : struct
    {

        public TValue Position { get; set; }
        public TValue Velocity { get; set; }
        public TValue Acceleration { get; set; }

        

        public PhysicsComponent()
        {

        }

        public abstract void Update(GameTime time);


    }

    public class PhysicsComponentVector : PhysicsComponent<Vector2> {


        public float GravityMultiplier { get; set; } = 2f;

        public override void Update(GameTime time)
        {
            Acceleration += Vector2.UnitY * GravityMultiplier; // its gravity!

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
    public class PhysicsComponentScalar : PhysicsComponent<float>
    {
        public override void Update(GameTime time)
        {
            Velocity += Acceleration;

            Velocity -= Velocity * .1f;

            Position += Velocity;

            Acceleration = 0;
        }
    }

}
