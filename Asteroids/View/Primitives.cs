using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.View
{
    public static class Primitives
    {
        public static GraphicsDevice Device;

        private static readonly Lazy<Texture2D> Pixel = new Lazy<Texture2D>(() =>
        {
            var p = new Texture2D(Device, 1, 1, false, SurfaceFormat.Color);
            p.SetData(new[] {Color.White});
            return p;
        });

        private static void DrawPixel(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 scale)
        {
            spriteBatch.Draw(Pixel.Value, position, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        private static void DrawPixel(SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 scale)
        {
            DrawPixel(spriteBatch, position, Color.White, rotation, scale);
        }

        public static void PutPixel(SpriteBatch spriteBatch, Vector2 position)
        {
            DrawPixel(spriteBatch, position, 0, Vector2.One);
        }

        public static void PutPixel(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            DrawPixel(spriteBatch, position, color, 0, Vector2.One);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            var angle = (float) Math.Atan2(dy, dx);
            var length = Vector2.Distance(start, end);
            DrawLine(spriteBatch, start, length, angle);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 position, float length, float angle)
        {
            DrawPixel(spriteBatch, position, angle, new Vector2(length, 1));
        }

        public static void DrawArrow(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            var dx = start.X - end.X;
            var dy = start.Y - end.Y;
            var angle = (float) Math.Atan2(dy, dx);
            var length = Vector2.Distance(start, end);
            const float ang = (float)Math.PI/6;
            DrawLine(spriteBatch, start, end);
            DrawLine(spriteBatch, end, length * 0.3f, angle - ang);
            DrawLine(spriteBatch, end, length * 0.3f, angle + ang);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            DrawLine(spriteBatch, start, new Vector2(start.X, end.Y));
            DrawLine(spriteBatch, new Vector2(start.X, end.Y), end);
            DrawLine(spriteBatch, end, new Vector2(end.X, start.Y));
            DrawLine(spriteBatch, new Vector2(end.X, start.Y), start);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            DrawRectangle(spriteBatch, new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Right, rectangle.Bottom));
        }

        public static void DrawPolygon(SpriteBatch spriteBatch, IEnumerable<Vector2> points)
        {
            var pointsArray = points as Vector2[] ?? points.ToArray();

            if (!pointsArray.Any())
                return;

            for (var i = 0; i < pointsArray.Length - 1; i++)
            {
                var p1 = pointsArray[i];
                var p2 = pointsArray[i + 1];
                DrawLine(spriteBatch, p1, p2);
            }
            DrawLine(spriteBatch, pointsArray[0], pointsArray.Last());
        }
    }
}
