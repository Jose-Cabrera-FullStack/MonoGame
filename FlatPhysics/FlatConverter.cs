using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using FlatPhysics;

namespace FlatPhysics
{
    public class FlatConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToVector2(FlatVector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}