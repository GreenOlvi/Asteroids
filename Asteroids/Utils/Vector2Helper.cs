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
    }
}
