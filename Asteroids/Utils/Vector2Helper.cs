using System;
using Microsoft.Xna.Framework;

namespace Asteroids.Utils
{
    public static class Vector2Helper
    {
        public static Vector2 VectorAtAngle(float angle, float length = 1)
        {
            return new Vector2(
                (float) Math.Sin(angle)*length,
                (float) -Math.Cos(angle)*length);
        }

        public static Vector2 WrapInside(Vector2 point, Rectangle rectangle)
        {
            if (rectangle.Contains(point))
                return point;

            return new Vector2(Mod(point.X, rectangle.Width) + rectangle.X,
                Mod(point.Y, rectangle.Height) + rectangle.Y);
        }

        private static float Mod(float x, int m)
        {
            var r = x%m;
            return r < 0 ? r + m : r;
        }
    }
}
