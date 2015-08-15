using Asteroids.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids
{
    class LaserProjectile : Entity
    {
        private static Sprite sprite;
        public static double maxSpriteLength;

        private const float VELOCITY = 10.0f;
    
        private float angle = 0.0f;
        public float Angle {
            get { return angle; }
            private set { angle = MathHelper.WrapAngle(value); }
        }

        public Vector2 Velocity { get; private set; }
        private Vector2 rotationOrigin = new Vector2(10, 10);

        public LaserProjectile(Vector2 position, float angle)
        {
            Position = position;
            Angle = angle;
            Velocity = new Vector2(
                Convert.ToSingle(Math.Sin(Angle) * VELOCITY),
                Convert.ToSingle(-Math.Cos(Angle) * VELOCITY));
        }
        public new static void LoadContent(ContentManager content)
        {
            sprite = new Sprite(content.Load<Texture2D>("laser"));
            maxSpriteLength = Math.Sqrt(sprite.Width ^ 2 + sprite.Height ^ 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position, Angle, rotationOrigin);
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;

            if (!GameInstance.Screen.Contains(Position))
            {
                Destroyed = true;
            }
        }

    }
}
