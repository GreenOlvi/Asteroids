using System;
using System.Linq;
using Asteroids.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class AsteroidDrawer : IEntityDrawer<Asteroid>
    {
        public EntityType Type => EntityType.Asteroid;

        public void LoadContent(ContentManager contentManager) { }

        public void Draw(SpriteBatch spriteBatch, Asteroid asteroid)
        {
            var cartesian = asteroid.Points
                .Select(x => new Vector2(x.X + asteroid.Angle, x.Y))
                .Select(x => asteroid.Position + Polar2Cartesian(x) * asteroid.Scale)
                .ToList();

            Primitives.DrawPolygon(spriteBatch, cartesian);
        }

        public void Draw(SpriteBatch spriteBatch, IEntity entity)
        {
            Draw(spriteBatch, (Asteroid) entity);
        }

        private static Vector2 Polar2Cartesian(Vector2 point)
        {
            return new Vector2((float) (point.Y * Math.Cos(point.X)), (float) (point.Y * -Math.Sin(point.X)));
        }
    }
}
