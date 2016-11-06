using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering.Character
{
    class Joint
    {
        //public Joint Parent { get; set; }
        //public Joint Child { get; set; }

        public Vector2 Base { get; set; }
        public float Length { get; set; }
        public float Angle { get; set; }

        public void Render(RenderHelper render)
        {
            render.DrawLine(Base, Base + Angle.ToVector() * Length, Color.Black, 4);
        }
    }
}
