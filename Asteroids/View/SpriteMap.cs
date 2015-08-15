using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.View
{
    class SpriteMap
    {
        public Texture2D Texture { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int SpriteWidth { get; private set; }
        public int SpriteHeight { get; private set; }
        public List<Rectangle> Sprites { get; private set; }

        public SpriteMap(Texture2D texture, int columns, int rows)
        {
            this.Texture = texture;
            this.Columns = columns;
            this.Rows = rows;

            this.SpriteWidth = texture.Width / columns;
            this.SpriteHeight = texture.Height / rows;

            var sprites = new List<Rectangle>(Columns * Rows);
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var rectangle = new Rectangle(SpriteWidth * col, SpriteHeight * row, SpriteWidth, SpriteHeight);
                    sprites.Add(rectangle);
                }
            }

            this.Sprites = sprites;
        }

        public void Draw(SpriteBatch spriteBatch, int sprite, Vector2 position) {
            var sourceRectangle = Sprites[sprite];
            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, int sprite, Vector2 position, float angle, Vector2 origin) {
            var sourceRectangle = Sprites[sprite];
            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, angle, origin, SpriteEffects.None, 0f);
        }

        public Sprite getSprite(int index)
        {
            return new Sprite(index, this, SpriteWidth, SpriteHeight);
        }
    }

    class Sprite
    {
        private int SpriteIndex;
        private SpriteMap Map;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Sprite(int spriteIndex, SpriteMap map, int width, int height)
        {
            SpriteIndex = spriteIndex;
            Map = map;
            Width = width;
            Height = height;
        }

        public Sprite(Texture2D texture)
        {
            SpriteIndex = 0;
            Map = new SpriteMap(texture, 1, 1);
            Width = Map.SpriteWidth;
            Height = Map.SpriteHeight;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Map.Draw(spriteBatch, SpriteIndex, position);
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position, float angle, Vector2 origin)
        {
            Map.Draw(spriteBatch, SpriteIndex, position, angle, origin);
        }
    }
}
