using System;

namespace Flat
{
    public class Util
    {
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException("min must be less than or equal to max");
            }

            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException("min must be less than or equal to max");
            }

            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }
    }
}