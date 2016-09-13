using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroids.View;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Model
{
    class Player : IEntity
    {
        private const int SPRITE_NORMAL = 0;
        private const int SPRITE_ACCELERATE = 1;

        private static Sprite[] Sprites;

        private const float ROTATE_SPEED = 0.1f;
        private const float ACCELERATION = 0.1f;

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public bool Destroyed { get; private set; }

        private float _angle = 0.0f;
        public float Angle {
            get { return _angle; }
            private set { _angle = MathHelper.WrapAngle(value); }
        }

        private Vector2 RotationOrigin { get; set; }
        public bool IsAccelerating { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player(Vector2 position)
        {
            Position = position;
            Velocity = new Vector2(0.0f, 0.0f);
            Angle = MathHelper.Pi;
            RotationOrigin = new Vector2(10, 15);
            IsAccelerating = false;
        }

        public static void LoadContent(ContentManager content)
        {
            var playerSprite = content.Load<Texture2D>(@"player");
            var playerMap = new SpriteMap(playerSprite, 2, 1);
            Sprites = new Sprite[2];
            Sprites[SPRITE_NORMAL] = playerMap.GetSprite(0);
            Sprites[SPRITE_ACCELERATE] = playerMap.GetSprite(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var spriteIndex = IsAccelerating ? SPRITE_ACCELERATE : SPRITE_NORMAL;
            Sprites[spriteIndex].Draw(spriteBatch, Position, Angle, RotationOrigin);
        }

        public void Rotate(float speed)
        {
            Angle += ROTATE_SPEED * speed;
        }

        public void RotateLeft(float speed = 1.0f)
        {
            Angle -= ROTATE_SPEED * speed;
        }

        public void RotateRight(float speed = 1.0f)
        {
            Angle += ROTATE_SPEED * speed;
        }

        public void Shoot()
        {
            EntityManager.Instance.Add(new LaserProjectile(Position, Angle));
        }

        public void Update(GameTime gameTime)
        {
            InputUpdate();

            if (IsAccelerating)
            {
                Velocity += new Vector2(Convert.ToSingle(Math.Sin(Angle) * ACCELERATION), Convert.ToSingle(-Math.Cos(Angle) * ACCELERATION));
            }
            Position = Position + Velocity;

            var screenWidth = AsteroidsGame.Instance.ScreenWidth;
            var screenHeight = AsteroidsGame.Instance.ScreenHeight;
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

        private void InputUpdate()
        {
            if (InputManager.Instance.KeyDown(Keys.Right))
                RotateRight();

            if (InputManager.Instance.KeyDown(Keys.Left))
                RotateLeft();

            Rotate(InputManager.Instance.GamePadLeftThumbStick(PlayerIndex.One).X);

            if (InputManager.Instance.KeyDown(Keys.Up) || InputManager.Instance.GamePadButtonDown(PlayerIndex.One, Buttons.A))
                IsAccelerating = true;
            else
                IsAccelerating = false;

            if (InputManager.Instance.KeyPressed(Keys.Space) || InputManager.Instance.GamePadButtonPressed(PlayerIndex.One, Buttons.RightTrigger))
                Shoot();
        }
    }
}
