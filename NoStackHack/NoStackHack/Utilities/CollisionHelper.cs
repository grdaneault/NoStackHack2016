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
            info.A = a;
            info.B = b;
            
            /* Use SAT.
             * 1. Get each axis, where an axis is the perpendicular of a side
             * 2. For each axis, project the shape onto the axis
             * 3. Check for 1-d intersection of the projections
             */

            // 1. get all axes
            // HACK: in the 2 boxes case, we know that all axis' will just be UNITX and UNITY
            var axes = new Vector2[] { Vector2.UnitX, Vector2.UnitY }.ToList();
            // REMEMBER, the axes **MUST** be normalized!!!

            // 2. project the shapes onto each axis
            var mostOverlap = 0f;
            var normal = Vector2.Zero;
            foreach(Vector2 axis in axes)
            {

                var normalFlipper = 1;
                // projection of shape A
                // HACK: because we know that A and B are boxes, their projections are simple

                var aPoints = a.GetPoints();
                var aMin = float.MaxValue;
                var aMax = float.MinValue;
                foreach(Vector2 point in aPoints)
                {
                    var value = Vector2.Dot(axis, point);
                    aMin = Math.Min(value, aMin);
                    aMax = Math.Max(value, aMax);
                }

                var bPoints = b.GetPoints();
                var bMin = float.MaxValue;
                var bMax = float.MinValue;
                foreach (Vector2 point in bPoints)
                {
                    var value = Vector2.Dot(axis, point);
                    bMin = Math.Min(value, bMin);
                    bMax = Math.Max(value, bMax);
                }

                // calculate overlap
                var overlap = 0f;

                //// check if a's min and max intersect with b's min and max
                if (aMin > bMin && aMax < bMax)
                {
                    overlap = (aMax - aMin);
                }
                if (bMin > aMin && bMax < aMax)
                {
                    overlap = (bMax - bMin);
                }
                if (aMax > bMin && aMin < bMin && aMax < bMax)
                {
                    overlap = aMax - bMin;
                }
                if (bMax > aMin && bMin < aMin && bMax < aMax)
                {
                    overlap = bMax - aMin;
                    //normalFlipper = -1;
                }

                if (aMin < bMax && aMin > bMin && aMax > bMax)
                {
                    normalFlipper = -1;
                }

                if (overlap > 0)
                {
                    info.IsColliding = true;

                    if (overlap > mostOverlap)
                    {
                        mostOverlap = overlap;
                        normal = axis * normalFlipper;
                    }

                }
            }

            info.Overlap = mostOverlap;
            info.Normal = normal;

            return info;
        }

        

    }

    public class CollisionInfo
    {
        public Box A { get; set; }
        public Box B { get; set; }
        public bool IsColliding { get; set; }
        public Vector2 Normal { get; set; }
        //public Vector2 Point { get; set; }
        public float Overlap { get; set; }
    }
}
