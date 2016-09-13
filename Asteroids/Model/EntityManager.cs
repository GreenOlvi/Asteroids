using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Model
{
    class EntityManager
    {
        private static EntityManager _instance;
        public static EntityManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EntityManager();

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        private List<IEntity> entities = new List<IEntity>();
        private List<IEntity> toAdd = new List<IEntity>(); 
        public int EntityCount => entities.Count;

        private EntityManager() { }

        public void Add(IEntity entity)
        {
            toAdd.Add(entity);
        }

        public void Clear()
        {
            entities.Clear();
        }

        public void Update(GameTime gameTime)
        {
            entities.AddRange(toAdd);
            toAdd.Clear();
            foreach (var entity in entities)
            {
                entity.Update(gameTime);
            }
            entities.RemoveAll(e => e.Destroyed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            entities.ForEach(e => e.Draw(spriteBatch));
        }
    }
}
