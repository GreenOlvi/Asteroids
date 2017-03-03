﻿using Asteroids.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids.Model
{
    public class LaserProjectile : IEntity
    {
        public EntityType Type => EntityType.LaserProjectile;

        private const float VELOCITY = 10.0f;
    
        private float _angle = 0.0f;
        public float Angle {
            get { return _angle; }
            private set { _angle = MathHelper.WrapAngle(value); }
        }

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }

        public bool Destroyed { get; private set; }

        public LaserProjectile(Vector2 position, float angle)
        {
            Position = position;
            Angle = angle;
            Velocity = new Vector2(
                Convert.ToSingle(Math.Sin(Angle) * VELOCITY),
                Convert.ToSingle(-Math.Cos(Angle) * VELOCITY));
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
