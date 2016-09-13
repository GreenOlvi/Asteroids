using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Model
{
    public interface IEntity
    {
        Vector2 Position { get; }
        bool Destroyed { get; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}