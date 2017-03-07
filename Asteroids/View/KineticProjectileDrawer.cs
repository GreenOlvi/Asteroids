using Asteroids.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class KineticProjectileDrawer : IEntityDrawer<KineticProjectile>
    {
        public EntityType Type => EntityType.KineticProjectile;

        public void LoadContent(ContentManager contentManager)
        {
        }

        public void Draw(SpriteBatch spriteBatch, IEntity entity)
        {
            Draw(spriteBatch, (KineticProjectile) entity);
        }

        public void Draw(SpriteBatch spriteBatch, KineticProjectile projectile)
        {
            Primitives.PutPixel(spriteBatch, projectile.Position);
        }
    }
}
