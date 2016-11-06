using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering
{
    public class Camera
    {
        public PhysicsComponentVector PhysicsComponent { get; set; } = new PhysicsComponentVector();
        public PhysicsComponentScalar ScaleComponent { get; set; } = new PhysicsComponentScalar();

        private Vector2 _targetPosition;
        private float _targetScale;

        public Vector2 ScreenDimension { get; set; }

        public Vector2 WorldTopLeft { get; set; }
        public Vector2 WorldBotRight { get; set; }

        public Camera(Vector2 screenDimension)
        {
            ScreenDimension = screenDimension;
            PhysicsComponent.Position = ScreenDimension / 2;
            PhysicsComponent.GravityMultiplier = 0;

            WorldTopLeft = Vector2.Zero;
            WorldBotRight = screenDimension;
        }

        public void Update(GameTime time)
        {
            ScaleComponent.Update(time);
            PhysicsComponent.Update(time);
        }

        public void TrackBoxes(List<Box> boxes, float xPad=200, float YPad=50)
        {
            var xMax = boxes.Max(b => b.Right + xPad);
            var xMin = boxes.Min(b => b.Left - xPad);

            var yMax = boxes.Max(b => b.Bottom + YPad);
            var yMin = boxes.Min(b => b.Top - YPad);

            var xMid = (xMax + xMin) / 2;
            var yMid = (yMax + yMin) / 2;

            var target = new Vector2(xMid, yMid);


            

            _targetPosition = target;

            var dist = new Vector2(xMax - xMin, yMax - yMin);

            _targetScale = Math.Max(
                dist.X / ScreenDimension.X,
                dist.Y / ScreenDimension.Y);

            _targetScale = Math.Min(2f, _targetScale);
            _targetScale = Math.Max(.8f, _targetScale);
            


            ScaleComponent.Acceleration = (_targetScale - ScaleComponent.Position) * .007f;
            PhysicsComponent.Acceleration = (_targetPosition - PhysicsComponent.Position) * .009f;

      


        }
        public void SetWorldConstrains(Vector2 topLeft)
        {
            WorldTopLeft = topLeft;
        }

        public Matrix GetTransform()
        {
            var half = ScreenDimension / 2;
            var t = Matrix.Identity;


            var padRatio = ScaleComponent.Position;
            var xPad = 1920 * padRatio;
            var yPad = 1080 * padRatio;

            var topLeft = PhysicsComponent.Position - new Vector2(xPad, yPad);
            var botRight = PhysicsComponent.Position + new Vector2(xPad, yPad);

            var position = (topLeft + botRight) / 2;


            if (position.X <= WorldTopLeft.X + xPad / 2)
            {
                position.X = WorldTopLeft.X + xPad / 2;

                if (position.X >= WorldBotRight.X - xPad / 2)
                {
                    position.X = (WorldBotRight.X - WorldTopLeft.X) / 2;
                }

            } else if (position.X >= WorldBotRight.X - xPad / 2)
            {
                position.X = WorldBotRight.X - xPad / 2;

                if (position.X <= WorldTopLeft.X + xPad / 2)
                {
                    position.X = (WorldBotRight.X - WorldTopLeft.X) / 2;
                }
            }

            if (position.Y <= WorldTopLeft.Y + yPad / 2)
            {
                position.Y = WorldTopLeft.Y + yPad / 2;

                if (position.Y >= WorldBotRight.Y - yPad / 2)
                {
                    position.Y = (WorldBotRight.Y - WorldTopLeft.Y) / 2;
                }

            }
            else if (position.Y >= WorldBotRight.Y - yPad / 2)
            {
                position.Y = WorldBotRight.Y - yPad / 2;

                if (position.Y <= WorldTopLeft.Y + yPad / 2)
                {
                    position.Y = (WorldBotRight.Y - WorldTopLeft.Y) / 2;
                }
            }


            var scale = 1/padRatio;

            t *= Matrix.CreateTranslation(-position.As3());
            t *= Matrix.CreateScale(scale);

     
            t *= Matrix.CreateTranslation(half.As3());

            return t;
        }
    }
}
