using Microsoft.Xna.Framework;

namespace Asteroids.Model
{
    public interface IEntity
    {
        EntityType Type { get; }
        Vector2 Position { get; }
        bool Destroyed { get; }

        void Update(GameTime gameTime);
    }
}