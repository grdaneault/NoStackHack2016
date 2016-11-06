using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Utilities
{
    public class Box
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public float Left { get { return Position.X; } }
        public float Right { get { return Position.X + Size.X; } }
        public float Top { get { return Position.Y; } }
        public float Bottom { get { return Position.Y + Size.Y; } }
        public float MiddleX { get { return Position.X + Size.X / 2; } }
        public Box(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
        public Box(float x, float y, float w, float h)
            : this(new Vector2(x, y), new Vector2(w, h))
        {

        }

        public Vector2[] GetPoints()
        {
            return new Vector2[]
            {
                Position,
                Position + Vector2.UnitX*Size.X,
                Position + Size,
                Position + Vector2.UnitY*Size.Y
            };
        }

    }
}
