using System;
using Microsoft.Xna.Framework;

namespace Asteroids.Utils
{
    public class PolarVector2
    {
        public float Angle { get; private set; }
        public float Radius { get; private set; }

        public PolarVector2(float angle, float radius)
        {
            Angle = angle;
            Radius = radius;
        }

        public Vector2 ToCartesian()
        {
            return new Vector2((float) (Radius * Math.Cos(Angle)),
                (float) (Radius * -Math.Sin(Angle)));
        }
    }
}
