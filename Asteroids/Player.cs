using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.View;
using Microsoft.Xna.Framework.Content;

namespace Asteroids
{
    class Player : Entity
    {
        private const int SPRITE_NORMAL = 0;
        private const int SPRITE_ACCELERATE = 1;

        private static Sprite[] Sprites;

        private const float ROTATE_SPEED = 0.1f;
        private const float ACCELERATION = 0.1f;

        public Vector2 Velocity { get; private set; }

        private float angle = 0.0f;
        public float Angle {
            get { return angle; }
            private set { angle = MathHelper.WrapAngle(value); }
        }

        private Vector2 RotationOrigin { get; set; }
        public bool isAccelerating { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player(Vector2 position)
        {
            Position = position;
            Velocity = new Vector2(0.0f, 0.0f);
            Angle = MathHelper.Pi;
            RotationOrigin = new Vector2(10, 15);
            isAccelerating = false;
        }

        public new static void LoadContent(ContentManager content)
        {
            var playerSprite = content.Load<Texture2D>(@"player");
            var playerMap = new SpriteMap(playerSprite, 2, 1);
            Sprites = new Sprite[2];
            Sprites[SPRITE_NORMAL] = playerMap.getSprite(0);
            Sprites[SPRITE_ACCELERATE] = playerMap.getSprite(1);
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            var spriteIndex = isAccelerating ? SPRITE_ACCELERATE : SPRITE_NORMAL;
            Sprites[spriteIndex].Draw(spriteBatch, Position, Angle, RotationOrigin);
        }

        public void RotateLeft(float speed = 1.0f)
        {
            Angle -= ROTATE_SPEED * speed;
        }

        public void RotateRight(float speed = 1.0f)
        {
            Angle += ROTATE_SPEED * speed;
        }

        public Entity Shoot()
        {
            return new LaserProjectile(Position, Angle);
        }

        override public void Update(GameTime gameTime)
        {
            if (isAccelerating)
            {
                Velocity += new Vector2(Convert.ToSingle(Math.Sin(Angle) * ACCELERATION), Convert.ToSingle(-Math.Cos(Angle) * ACCELERATION));
            }
            Position = Position + Velocity;

            var screenWidth = GameInstance.ScreenWidth;
            var screenHeight = GameInstance.ScreenHeight;
            var x = Position.X;
            var y = Position.Y;

            if (x < 0)
                x += screenWidth;

            if (x > screenWidth)
                x %= screenWidth;

            if (y < 0)
                y += screenHeight;

            if (y > screenHeight)
                y %= screenHeight;

            Position = new Vector2(x, y);
        }
    }
}
