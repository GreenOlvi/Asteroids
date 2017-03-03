using System;
using Asteroids.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class LaserProjectileDrawer : IEntityDrawer<LaserProjectile>
    {
        public EntityType Type => EntityType.LaserProjectile;

        private static Sprite _sprite;
        private static double _maxSpriteLength;

        private readonly Vector2 _rotationOrigin = new Vector2(10, 10);

        public void LoadContent(ContentManager contentManager)
        {
            _sprite = new Sprite(contentManager.Load<Texture2D>("laser"));
            _maxSpriteLength = Math.Sqrt(Math.Pow(_sprite.Width, 2) + Math.Pow(_sprite.Height, 2));
        }

        public void Draw(SpriteBatch spriteBatch, IEntity entity)
        {
            Draw(spriteBatch, (LaserProjectile) entity);
        }

        public void Draw(SpriteBatch spriteBatch, LaserProjectile laserProjectile)
        {
            _sprite.Draw(spriteBatch, laserProjectile.Position, laserProjectile.Angle, _rotationOrigin);
        }
    }
}
