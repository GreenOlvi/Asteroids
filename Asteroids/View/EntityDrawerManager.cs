using System.Collections.Generic;
using Asteroids.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class EntityDrawerManager
    {
        private readonly PlayerDrawer _playerDrawer;
        private readonly AsteroidDrawer _asteroidDrawer;
        private readonly LaserProjectileDrawer _laserProjectileDrawer;

        public EntityDrawerManager(PlayerDrawer playerDrawer, AsteroidDrawer asteroidDrawer, LaserProjectileDrawer laserProjectileDrawer)
        {
            _playerDrawer = playerDrawer;
            _asteroidDrawer = asteroidDrawer;
            _laserProjectileDrawer = laserProjectileDrawer;
        }

        public void LoadContent(ContentManager contentManager)
        {
            _playerDrawer.LoadContent(contentManager);
            _asteroidDrawer.LoadContent(contentManager);
            _laserProjectileDrawer.LoadContent(contentManager);
        }

        private void Draw(SpriteBatch spriteBatch, IEntity entity)
        {
            switch (entity.Type)
            {
                case EntityType.Player:
                    _playerDrawer.Draw(spriteBatch, entity);
                    break;
                case EntityType.Asteroid:
                    _asteroidDrawer.Draw(spriteBatch, entity);
                    break;
                case EntityType.LaserProjectile:
                    _laserProjectileDrawer.Draw(spriteBatch, entity);
                    break;
            }
        }

        public void DrawEntities(SpriteBatch spriteBatch, IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                Draw(spriteBatch, entity);
            }
        }
    }
}
