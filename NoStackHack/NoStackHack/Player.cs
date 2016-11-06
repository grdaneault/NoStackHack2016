using Microsoft.Xna.Framework;
using NoStackHack.ControlInput;
using NoStackHack.Utilities;
using NoStackHack.Rendering.Character;
using System.Collections.Generic;
using NoStackHack.Rendering;
using NoStackHack.PlayerState;

namespace NoStackHack
{
    public class Player : IJumper, IMovable, IResetable
    {
        public PhysicsComponentVector PhysicsComponent { get; private set; }

        public VisualComponent VisualComponent { get; private set; }

        public Box Box { get { return new Box(PhysicsComponent.Position, new Vector2(50, 100)); } }

        public List<Box> Boxes;
        
        private double _tickers = 0;
        public PlayerStateMachine PlayerState;

        public Player(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
            PhysicsComponent = new PhysicsComponentVector();
            PhysicsComponent.Position = new Vector2(700, 100);
            VisualComponent = new VisualComponent(this);
            PlayerState = new PlayerStateMachine(new IdleState(this));
        }

        public void Move(Vector2 direction)
        {
            PlayerState.ActiveState.Move(direction);
        }

        public void Jump(float power)
        {
            PlayerState.ActiveState.Jump(power);
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
            Boxes = boxes;
            PlayerState.Update(time);
        }

        public void Render(RenderHelper render)
        {
            render.DrawBox(Box, Color.FromNonPremultiplied(128, 128, 128, 128));
            VisualComponent.Render(render);
        }
    }
}
