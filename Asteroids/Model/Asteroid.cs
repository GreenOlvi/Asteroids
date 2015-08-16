using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Model
{
    class Asteroid : Entity
    {
        private static readonly int N_MIN = 10;
        private static readonly int N_MAX = 20;
        private static readonly double R_MIN = 0.3;
        private static readonly double R_MAX = 1.0;

        private Random rng;
        public float Scale { get; private set; }

        private float angle = 0.0f;
        public float Angle {
            get { return angle; }
            private set { angle = MathHelper.WrapAngle(value); }
        }

        public List<Vector2> Points { get; private set; }

        public Asteroid(Vector2 position)
        {
            Position = position;
            rng = new Random();
            Scale = 100.0f;
            Generate();
        }

        private void Generate()
        {
            var n = rng.Next(N_MIN, N_MAX);
            Console.WriteLine("n={0}", n);

            var angles = GenerateAngles(n);
            var radius = Enumerable.Range(0, n).Select(x => (rng.NextDouble() * (R_MAX - R_MIN)) + R_MIN).ToList();

            Points = Enumerable.Range(0, n).Select(x => new Vector2((float)angles[x], (float)radius[x])).ToList();
        }

        private List<double> GenerateAngles(int n)
        {
            var angles = Enumerable.Range(0, n).Select(x => rng.NextDouble() + 0.2);
            var norm = MathHelper.TwoPi / angles.Sum();
            var normalized = angles.Select(a => a * norm).ToList();

            var add = new List<double>();
            var sum = 0.0;
            foreach (var a in normalized)
            {
                sum += a;
                add.Add(sum);
            }

            return add;
        }

        public override void Update(GameTime gameTime)
        {
            Angle += 0.01f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var dist = new Vector2(Scale, Scale);

            var cartesian = Points
                .Select(x => new Vector2(x.X + Angle, x.Y))
                .Select(x => Position + Polar2Cartesian(x) * Scale)
                .ToList();
            Primitives.DrawPolygon(spriteBatch, cartesian);
        }

        private static Vector2 Polar2Cartesian(Vector2 point)
        {
            return new Vector2((float) (point.Y * Math.Cos(point.X)), (float) (point.Y * -Math.Sin(point.X)));
        }
    }
}
