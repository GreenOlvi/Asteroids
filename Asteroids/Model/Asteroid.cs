using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Model
{
    public class Asteroid : IEntity
    {
        public EntityType Type => EntityType.Asteroid;

        private const int N_MIN = 10;
        private const int N_MAX = 20;
        private const double R_MIN = 0.3;
        private const double R_MAX = 1.0;

        public float Scale { get; private set; } = 100.0f;

        private float _angle = 0.0f;
        public float Angle {
            get { return _angle; }
            private set { _angle = MathHelper.WrapAngle(value); }
        }

        public List<Vector2> Points { get; private set; }
        public Vector2 Position { get; private set; }
        public bool Destroyed { get; private set; } = false;

        private Asteroid(Vector2 position, List<Vector2> points)
        {
            Position = position;
            Points = points;
        }

        public static Asteroid GenerateRandom(Vector2 position, Random rng = null)
        {
            if (rng == null)
                rng = new Random();

            var n = rng.Next(N_MIN, N_MAX);
            Console.WriteLine("n={0}", n);

            var angles = GenerateAngles(n, rng);
            var radius = Enumerable.Range(0, n).Select(x => (rng.NextDouble() * (R_MAX - R_MIN)) + R_MIN).ToList();

            var points = Enumerable.Range(0, n).Select(x => new Vector2((float)angles[x], (float)radius[x])).ToList();

            return new Asteroid(position, points);
        }

        private static List<double> GenerateAngles(int n, Random rng)
        {
            var angles = Enumerable.Range(0, n).Select(x => rng.NextDouble() + 0.2).ToList();
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

        public void Update(GameTime gameTime)
        {
            Angle += 0.01f;
        }
    }
}
