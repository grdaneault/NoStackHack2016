using System;
using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;
using NoStackHack.Rendering.Character;
using System.Collections.Generic;
using NoStackHack.Rendering;

namespace NoStackHack
{
    public class Player : IJumper, IMovable, IResetable
    {
        public PhysicsComponentVector PhysicsComponent { get; private set; }

        public VisualComponent VisualComponent { get; private set; }

        public Box Box { get { return new Box(PhysicsComponent.Position, new Vector2(50, 100)); } }

        private double _tickers = 0;

        public Player()
        {
            PlayerIndex = PlayerIndex.One;
            PhysicsComponent = new PhysicsComponentVector();
            PhysicsComponent.Position = new Vector2(500, 500);
            VisualComponent = new VisualComponent(this);
        }

        public Player(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
            PhysicsComponent = new PhysicsComponentVector();
            PhysicsComponent.Position = new Vector2(500, 500);
            VisualComponent = new VisualComponent(this);
        }

        public void Move(Vector2 direction)
        {
            //PhysicsComponent.Acceleration += new Vector2(3 * direction.X * ( (-Math.Sign(direction.X) * .2f) + (float)Math.Sin(PhysicsComponent.Velocity.X) ) , 0);

            //if (Math.Abs( PhysicsComponent.Velocity.X) < .5f)
            //{
            //    _tickers = 0;
            //}
            //_tickers += .15;
            //PhysicsComponent.Acceleration += Vector2.UnitX * (direction.X * (float) Math.Abs(Math.Sin(_tickers)));

            PhysicsComponent.Acceleration += Vector2.UnitX * direction.X;
        }

        public void Jump(float power)
        {
            PhysicsComponent.Acceleration -= Vector2.UnitY * 10 * power;
        }

        public void ResetPosition()
        {
            PhysicsComponent.Position = new Vector2(500, 500);
            PhysicsComponent.Acceleration = Vector2.Zero;
        }

        public PlayerIndex PlayerIndex
        {
            get; private set;
        }

        public void Update(GameTime time, List<Box> boxes)
        {

            PhysicsComponent.Update(time);
            VisualComponent.Update(time);

            // World physics. This should probably be moved
            foreach (Box box in boxes)
            {
                var info = CollisionHelper.CollisionInfo(Box, box);
                if (info.IsColliding)
                {
                    PhysicsComponent.Position -= info.Normal * info.Overlap;
                    PhysicsComponent.Velocity = Vector2.Reflect(PhysicsComponent.Velocity, info.Normal) * new Vector2(1f, 0.5f); // debounce the player somewhat
                    //player.Acceleration -= info.Normal * info.Overlap;
                }
            }
        }

        public void Render(RenderHelper render)
        {
            render.DrawBox(Box, Color.FromNonPremultiplied(128, 128, 128, 128));
            VisualComponent.Render(render);
        }

    
    }
}
