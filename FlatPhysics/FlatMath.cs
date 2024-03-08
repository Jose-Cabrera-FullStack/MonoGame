using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatPhysics
{
    public class FlatMath
    {
        public static float Length(FlatVector vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static float Distance(FlatVector a, FlatVector b)
        {
            return Length(a - b);
        }

        public static FlatVector Normalize(FlatVector vector)
        {
            float length = Length(vector);
            return new FlatVector(vector.X / length, vector.Y / length);
        }

        public static float Dot(FlatVector a, FlatVector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Cross(FlatVector a, FlatVector b)
        {
            return a.X * b.Y - a.Y * b.X;
        }
    }
}