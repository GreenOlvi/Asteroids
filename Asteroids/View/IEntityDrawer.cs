using Asteroids.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public interface IEntityDrawer<in T> where T : IEntity
    {
        EntityType Type { get; }

        void LoadContent(ContentManager contentManager);
        void Draw(SpriteBatch spriteBatch, IEntity entity);
        void Draw(SpriteBatch spriteBatch, T entity);
    }
}
