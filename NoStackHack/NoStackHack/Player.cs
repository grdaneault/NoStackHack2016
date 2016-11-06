using System;
using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;
using NoStackHack.Rendering.Character;
using System.Collections.Generic;
using NoStackHack.Rendering;

namespace NoStackHack
{
    class Player : IJumper, IMovable, IResetable
    {
        public PhysicsComponent PhysicsComponent { get; private set; }

        public VisualComponent VisualComponent { get; private set; }

        public Box Box { get { return new Box(PhysicsComponent.Position, new Vector2(50, 100)); } }


        public Player()
        {
            PlayerIndex = PlayerIndex.One;
            PhysicsComponent = new PhysicsComponent();
            PhysicsComponent.Position = new Vector2(500, 500);
            VisualComponent = new VisualComponent();
        }

        public Player(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
            PhysicsComponent = new PhysicsComponent();
            PhysicsComponent.Position = new Vector2(500, 500);
            VisualComponent = new VisualComponent();
        }

        public void Move(Vector2 direction)
        {
            PhysicsComponent.Acceleration += new Vector2(direction.X, 0);
            
        }

        public void Jump()
        {
            PhysicsComponent.Acceleration -= Vector2.UnitY * 10;
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
                    PhysicsComponent.Velocity = Vector2.Reflect(PhysicsComponent.Velocity, info.Normal);
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
