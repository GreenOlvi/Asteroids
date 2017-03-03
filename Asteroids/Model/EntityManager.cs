using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Asteroids.Model
{
    public class EntityManager
    {
        public List<IEntity> Entities { get; } = new List<IEntity>();
        private readonly List<IEntity> _toAdd = new List<IEntity>(); 
        public int EntityCount => Entities.Count;

        public void Add(IEntity entity)
        {
            _toAdd.Add(entity);
        }

        public void Clear()
        {
            Entities.Clear();
        }

        public void Update(GameTime gameTime)
        {
            Entities.AddRange(_toAdd);
            _toAdd.Clear();

            foreach (var entity in Entities)
            {
                entity.Update(gameTime);
            }

            Entities.RemoveAll(e => e.Destroyed);
        }
    }
}
