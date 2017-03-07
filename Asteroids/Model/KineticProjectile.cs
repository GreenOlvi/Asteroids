using Asteroids.Utils;
using Microsoft.Xna.Framework;

namespace Asteroids.Model
{
    public class KineticProjectile : IEntity
    {
        public EntityType Type => EntityType.KineticProjectile;

        private const float VELOCITY = 7.0f;

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public bool Destroyed { get; private set; } = false;

        public KineticProjectile(Vector2 position, float angle)
        {
            Position = position;
            Velocity = Vector2Helper.VectorAtAngle(angle, VELOCITY);
        }

        public void Update(GameTime gameTime)
        {
            var acceleration = AsteroidsGame.Instance.Gravity.ApplyGravity(this);
            Velocity += acceleration;

            Position += Velocity;

            if (!AsteroidsGame.Instance.Screen.Contains(Position))
            {
                Destroyed = true;
            }
        }
    }
}
