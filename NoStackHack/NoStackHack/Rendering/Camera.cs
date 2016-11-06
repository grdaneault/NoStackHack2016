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

        public Vector2 ScreenDimension { get; set; }

        public Vector2 WorldTopLeft { get; set; }
        public Vector2 WorldBotRight { get; set; }

        public Camera(Vector2 screenDimension)
        {
            ScreenDimension = screenDimension;
            PhysicsComponent.Position = ScreenDimension / 2;


            WorldTopLeft = Vector2.Zero;
            WorldBotRight = screenDimension;
        }

        public void Update(GameTime time)
        {
            PhysicsComponent.Update(time);
        }

        public void TrackBoxes(List<Box> boxes, float xPad=250, float YPad=300)
        {
            var xMax = boxes.Max(b => b.Right + xPad);
            var xMin = boxes.Min(b => b.Left - xPad);

            var yMax = boxes.Max(b => b.Bottom + YPad);
            var yMin = boxes.Min(b => b.Top - YPad);

            var xMid = (xMax + xMin) / 2;
            var yMid = (yMax + yMin) / 2;

            var target = new Vector2(xMid, yMid);
            PhysicsComponent.Position = target;

            var dist = new Vector2(xMax - xMin, yMax - yMin);

            ScaleComponent.Position = Math.Max(
                dist.X / ScreenDimension.X,
                dist.Y / ScreenDimension.Y);

            ScaleComponent.Position = Math.Min(2f, ScaleComponent.Position);
            ScaleComponent.Position = Math.Max(.4f, ScaleComponent.Position);

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

            position.X = Math.Max(WorldTopLeft.X + xPad/2, position.X);
            position.Y = Math.Max(WorldTopLeft.Y + yPad/2, position.Y);

            position.X = Math.Min(WorldBotRight.X - xPad / 2, position.X);
            position.Y = Math.Min(WorldBotRight.Y - yPad / 2, position.Y);



            var scale = 1/padRatio;

            t *= Matrix.CreateTranslation(-position.As3());
            t *= Matrix.CreateScale(scale);

     
            t *= Matrix.CreateTranslation(half.As3());

            return t;
        }
    }
}
