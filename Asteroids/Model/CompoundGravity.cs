using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Asteroids.Model
{
    public class CompoundGravity : IGravitySource
    {
        public CompoundGravity()
        {
            GravitySources = new List<IGravitySource>();
        }

        private List<IGravitySource> GravitySources { get; }

        public void AddSource(IGravitySource source)
        {
            GravitySources.Add(source);
        }

        public Vector2 ApplyGravity(IEntity entity)
        {
            return GravitySources.Aggregate(Vector2.Zero,
                (v, source) => v + source.ApplyGravity(entity));
        }
    }
}
