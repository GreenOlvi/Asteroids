using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Model
{
    class EntityManager
    {
        private static EntityManager instance;
        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManager();

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private List<Entity> entities = new List<Entity>();
        public int EntityCount {
            get { return entities.Count; }
        }

        private EntityManager() { }

        public void Add(Entity entity)
        {
            entities.Add(entity);
        }

        public void Update(GameTime gameTime)
        {
            entities.ForEach(e => e.Update(gameTime));
            entities.RemoveAll(e => e.Destroyed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            entities.ForEach(e => e.Draw(spriteBatch));
        }
    }
}
