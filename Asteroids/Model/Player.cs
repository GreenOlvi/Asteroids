using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Model
{
    public class Player : IEntity
    {
        public EntityType Type => EntityType.Player;

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

        public bool IsAccelerating { get; private set; }

        public Player(Vector2 position)
        {
            Position = position;
            Velocity = new Vector2(0.0f, 0.0f);
            Angle = MathHelper.Pi;
            IsAccelerating = false;
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
            AsteroidsGame.Instance.AddEntity(new LaserProjectile(Position, Angle));
        }

        public void Update(GameTime gameTime)
        {
            InputUpdate();

            var acceleration = Vector2.Zero;

            if (IsAccelerating)
            {
                acceleration += new Vector2(Convert.ToSingle(Math.Sin(Angle) * ACCELERATION), Convert.ToSingle(-Math.Cos(Angle) * ACCELERATION));
            }

            acceleration += AsteroidsGame.Instance.Gravity.ApplyGravity(this);

            Velocity += acceleration;

            Position = Vector2.Add(Position, Velocity);

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
