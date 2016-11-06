using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;
using System.Collections.Generic;

namespace NoStackHack.PlayerState
{
    public abstract class DefaultState : IPlayerState, IJumper, IMovable
    {
        public Player Player { get; set; }
        protected IPlayerState NextState;

        public DefaultState(Player player)
        {
            Player = player;
        }

        public virtual void Exit() { }

        public virtual void Init() { }

        public virtual void Jump(float power)
        {
            NextState = new JumpingState(Player, power);
        }

        public virtual void Move(Vector2 translation)
        {
            Player.PhysicsComponent.Acceleration += Vector2.UnitX * translation.X;
        }

        public virtual IState Update(GameTime gameTime)
        {
            CheckCollisions();
            Player.VisualComponent.Update(gameTime);
            Player.PhysicsComponent.Update(gameTime);
            return NextState;
        }

        public virtual void CheckCollisions()
        {
            Collisions.Clear();
            // World physics. This should probably be moved
            foreach (Box box in Player.Boxes)
            {
                var info = CollisionHelper.CollisionInfo(Player.Box, box);
                if (info.IsColliding)
                {
                    Player.PhysicsComponent.Position -= info.Normal * info.Overlap;
                    Player.PhysicsComponent.Velocity = Vector2.Reflect(Player.PhysicsComponent.Velocity, info.Normal) * new Vector2(1f, 0.5f); // debounce the player somewhat
                    //player.Acceleration -= info.Normal * info.Overlap;
                    Collisions.Add(info);
                }
            }
        }

        public List<CollisionInfo> Collisions { get; set; } = new List<CollisionInfo>();
    }
}
