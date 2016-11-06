using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering.Character
{
    class Apendage
    {

        public Vector2 BasePosition { get; set; }
        public Vector2 TipPosition { get; set; }

        public float JointLength { get; set; }

        public bool UsePositive { get; set; } = true;

        public Vector2 Middle
        {
            get
            {
                var to = (TipPosition - BasePosition);
                var toUnit = to.Normal();

                var toPerpUnit = toUnit.Perpendicular();

                var commonPart = BasePosition + (to / 2);
                var negatablePart = toPerpUnit * (float)Math.Sqrt((JointLength * JointLength) - (to/2).LengthSquared());

                var s0 = commonPart + negatablePart;
                var s1 = commonPart - negatablePart;

                return (UsePositive ? s0 : s1);

            }
        }

        public void Render(RenderHelper render)
        {
            var middle = Middle;
            render.DrawLine(BasePosition, middle, Color.Black, 3);
            render.DrawLine(middle, TipPosition, Color.Black, 3);

            //render.DrawLine(middle, middle + Vector2.UnitX * JointLength, Color.White, 2);
            //render.DrawLine(TipPosition, TipPosition -  Vector2.UnitY * JointLength, Color.Red, 2);
            //render.DrawLine(BasePosition, BasePosition + Vector2.UnitY * JointLength, Color.Blue, 2);
        }

    }
}
