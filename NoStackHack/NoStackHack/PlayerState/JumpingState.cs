using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NoStackHack.Utilities;

namespace NoStackHack.PlayerState
{
    class JumpingState : DefaultState
    {
        public JumpingState(Player player, float power) : base(player)
        {
            Power = power;
        }

        public float Power;

        public override void Init()
        {
            Player.PhysicsComponent.Velocity *= Vector2.UnitX; 
            Player.PhysicsComponent.Acceleration = -Vector2.UnitY * (30 + Power);
        }

        public override IState Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach(var collision in Collisions)
            {
                if (collision.Normal.Equals(Vector2.UnitY))
                {
                    NextState = new IdleState(Player);
                }
            }

            return NextState;
        }

        public override void Jump(float power)
        {
            Counter++;
            if (Counter <= maxCount)
            {
                Player.PhysicsComponent.Acceleration += -Vector2.UnitY * 2;
            }
            // nothing
        }

        public int Counter { get; set; } = 0;
        private int maxCount = 50;
    }
}
