using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Utilities
{
    public static class Vector2Extensions
    {
        public static Vector2 Perpendicular(this Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }
        public static Vector2 Normal(this Vector2 v)
        {
            return new Vector2(v.X, v.Y) / v.Length();
        }
        public static Vector3 As3(this Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0);
        }

        public static Vector2 ToVector(this float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
