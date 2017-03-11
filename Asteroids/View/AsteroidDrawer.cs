using Asteroids.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class AsteroidDrawer : IEntityDrawer<Asteroid>
    {
        public EntityType Type => EntityType.Asteroid;

        public void LoadContent(ContentManager contentManager) { }

        public void Draw(SpriteBatch spriteBatch, Asteroid asteroid)
        {
            Primitives.DrawPolygon(spriteBatch, asteroid.Points);
        }

        public void Draw(SpriteBatch spriteBatch, IEntity entity)
        {
            Draw(spriteBatch, (Asteroid) entity);
        }
    }
}
