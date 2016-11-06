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

        public Vector2 TopLeft { get; set; }

        public Camera(Vector2 screenDimension)
        {
            ScreenDimension = screenDimension;
            PhysicsComponent.Position = ScreenDimension / 2;
            
        }

        public void Update(GameTime time)
        {
            PhysicsComponent.Update(time);
        }

        public void TrackBoxes(List<Box> boxes)
        {
            var xMax = boxes.Max(b => b.Right);
            var xMin = boxes.Min(b => b.Left);

            var yMax = boxes.Max(b => b.Bottom);
            var yMin = boxes.Min(b => b.Top);

            var xMid = (xMax + xMin) / 2;
            var yMid = (yMax + yMin) / 2;

            var target = new Vector2(xMid, yMid);

            var dist = new Vector2(xMax - xMin, yMax - yMin);
            ScaleComponent.Position = dist.Length() / ScreenDimension.Length();
            ScaleComponent.Position = .5f / ScaleComponent.Position;

            ScaleComponent.Position = Math.Min(1.9f, ScaleComponent.Position);
            ScaleComponent.Position = Math.Max(.5f, ScaleComponent.Position);

            PhysicsComponent.Position = target;
        }
        public void SetWorldConstrains(Vector2 topLeft)
        {
            TopLeft = topLeft;
        }

        public Matrix GetTransform()
        {
            var half = ScreenDimension / 2;
            var t = Matrix.Identity;

            var position = new Vector3();
            position.X = -PhysicsComponent.Position.X;
            position.Y = -PhysicsComponent.Position.Y;




            t *= Matrix.CreateTranslation(position+half.As3());
            t *= Matrix.CreateTranslation(-half.As3());

            t *= Matrix.CreateScale(ScaleComponent.Position);

            t *= Matrix.CreateTranslation(half.As3());


            //t *= Matrix.CreateTranslation(half.As3());
            //t *= Matrix.CreateScale(2f);

            return t;
        }
    }
}
