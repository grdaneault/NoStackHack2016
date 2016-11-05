using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Utilities
{
    public static class CollisionHelper
    {

        public static CollisionInfo CollisionInfo(Box a, Box b)
        {
            var info = new CollisionInfo();

            return info;
        }

    }

    public class CollisionInfo
    {
        public Box A { get; set; }
        public Box B { get; set; }
        public bool IsColliding { get; set; }
        public Vector2 Normal { get; set; }
        public Vector2 Point { get; set; }
        public float Overlap { get; set; }
    }
}
