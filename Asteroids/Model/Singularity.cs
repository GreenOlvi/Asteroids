using Microsoft.Xna.Framework;

namespace Asteroids.Model
{
    public class Singularity : IEntity, IGravitySource
    {
        public EntityType Type => EntityType.Singularity;

        public Vector2 Position { get; private set; }
        public float Force { get; private set; }
        public bool Destroyed { get; private set; }

        public Singularity(Vector2 position, float force)
        {
            Position = position;
            Force = force;
        }

        public void Update(GameTime gameTime)
        {
        }

        public Vector2 ApplyGravity(IEntity entity)
        {
            var f = Force / Vector2.DistanceSquared(Position, entity.Position);

            return Vector2.Normalize(Vector2.Subtract(Position, entity.Position))*f;
        }
    }
}
