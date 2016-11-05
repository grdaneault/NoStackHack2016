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
        public Box(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
    }
}
