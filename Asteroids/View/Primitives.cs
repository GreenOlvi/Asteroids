using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Asteroids.View
{
    class Primitives
    {
        public static GraphicsDevice graphicsDevice;

        private static Texture2D pixel;
        private static Texture2D Pixel {
            get
            {
                if (pixel == null)
                {
                    pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                    pixel.SetData(new[] { Color.White  });
                }

                return pixel;
            }
        }

        public static void PutPixel(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Pixel, position, null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            var angle = (float) Math.Atan2(dy, dx);
            var length = (float) Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            DrawLine(spriteBatch, start, length, angle);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 position, float length, float angle)
        {
            spriteBatch.Draw(Pixel, position, null, Color.White, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }
    }
}
