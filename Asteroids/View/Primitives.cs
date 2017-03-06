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

        public static void PutPixel(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Pixel.Value, position, null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
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
            spriteBatch.Draw(Pixel.Value, position, null, Color.White, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        public static void DrawArrow(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            var dx = start.X - end.X;
            var dy = start.Y - end.Y;
            var angle = (float) Math.Atan2(dy, dx);
            var length = Vector2.Distance(start, end);
            var ang = (float)Math.PI/6;
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

        public static void DrawPolygon(SpriteBatch spriteBatch, List<Vector2> points)
        {
            if (points.Count > 0)
            {
                for (var i = 0; i < points.Count - 1; i++)
                {
                    var p1 = points[i];
                    var p2 = points[i + 1];
                    DrawLine(spriteBatch, p1, p2);
                }
                DrawLine(spriteBatch, points[0], points.Last());
            }
        }
    }
}
