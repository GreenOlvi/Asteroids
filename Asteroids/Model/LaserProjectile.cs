using Asteroids.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids.Model
{
    class LaserProjectile : IEntity
    {
        private static Sprite _sprite;
        public static double MaxSpriteLength;

        private const float VELOCITY = 10.0f;
    
        private float _angle = 0.0f;
        public float Angle {
            get { return _angle; }
            private set { _angle = MathHelper.WrapAngle(value); }
        }

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        private Vector2 rotationOrigin = new Vector2(10, 10);

        public bool Destroyed { get; private set; }

        public LaserProjectile(Vector2 position, float angle)
        {
            Position = position;
            Angle = angle;
            Velocity = new Vector2(
                Convert.ToSingle(Math.Sin(Angle) * VELOCITY),
                Convert.ToSingle(-Math.Cos(Angle) * VELOCITY));
        }

        public static void LoadContent(ContentManager content)
        {
            _sprite = new Sprite(content.Load<Texture2D>("laser"));
            MaxSpriteLength = Math.Sqrt(Math.Pow(_sprite.Width, 2) + Math.Pow(_sprite.Height, 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, Position, Angle, rotationOrigin);
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;

            if (!AsteroidsGame.Instance.Screen.Contains(Position))
            {
                Destroyed = true;
            }
        }

    }
}
