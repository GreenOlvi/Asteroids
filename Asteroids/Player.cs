using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class Player
    {
        const float ROTATE_SPEED = 0.05f;

        public static Texture2D Sprite;

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        private float angle = 0.0f;
        public float Angle {
            get { return angle; }
            private set { this.angle = value % (2 * MathHelper.Pi); }
        }
        private Vector2 RotationOrigin { get; set; }

        public Player(Vector2 position)
        {
            Position = position;
            Velocity = new Vector2(0.0f, 0.0f);
            Angle = MathHelper.Pi;
            RotationOrigin = new Vector2(10, 15);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, Angle, RotationOrigin, 1.0f, SpriteEffects.None, 0f);
        }

        public void RotateLeft()
        {
            Angle -= ROTATE_SPEED;
        }

        public void RotateRight()
        {
            Angle += ROTATE_SPEED;
        }

        public void Update()
        {
            Position = Position + Velocity;
        }
    }
}
