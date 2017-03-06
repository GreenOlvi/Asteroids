using Microsoft.Xna.Framework;

namespace Asteroids.Model
{
    public interface IGravitySource
    {
        Vector2 ApplyGravity(IEntity entity);
    }
}
