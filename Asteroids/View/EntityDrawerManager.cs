using System.Collections.Generic;
using Asteroids.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class EntityDrawerManager
    {
        private readonly IEntityDrawer<Player> _playerDrawer;
        private readonly IEntityDrawer<Asteroid> _asteroidDrawer;
        private readonly IEntityDrawer<LaserProjectile> _laserProjectileDrawer;

        public EntityDrawerManager(IEntityDrawer<Player> playerDrawer, IEntityDrawer<Asteroid> asteroidDrawer, IEntityDrawer<LaserProjectile> laserProjectileDrawer)
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
                case EntityType.Singularity:
                case EntityType.KineticProjectile:
                    Primitives.PutPixel(spriteBatch, entity.Position);
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
