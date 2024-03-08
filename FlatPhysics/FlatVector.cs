using System;

namespace FlatPhysics
{
    public readonly struct FlatVector
    {
        public readonly float X;
        public readonly float Y;

        public static readonly FlatVector Zero = new FlatVector(0f, 0f);

        public FlatVector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static FlatVector operator +(FlatVector a, FlatVector b)
        {
            return new FlatVector(a.X + b.X, a.Y + b.Y);
        }

        public static FlatVector operator -(FlatVector a, FlatVector b)
        {
            return new FlatVector(a.X - b.X, a.Y - b.Y);
        }

        public static FlatVector operator -(FlatVector a)
        {
            return new FlatVector(-a.X, -a.Y);
        }

        public static FlatVector operator *(FlatVector a, FlatVector b)
        {
            return new FlatVector(a.X * b.X, a.Y * b.Y);
        }

        public static FlatVector operator /(FlatVector a, FlatVector b)
        {
            return new FlatVector(a.X / b.X, a.Y / b.Y);
        }

        public bool Equal(FlatVector other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return (obj is FlatVector vector && this.Equal(vector)) || false;
        }

        public override int GetHashCode()
        {
            return new { this.X, this.Y }.GetHashCode();
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }
}