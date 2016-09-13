using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
            Texture = texture;
            Columns = columns;
            Rows = rows;

            SpriteWidth = texture.Width / columns;
            SpriteHeight = texture.Height / rows;

            var sprites = new List<Rectangle>(Columns * Rows);
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    var rectangle = new Rectangle(SpriteWidth * col, SpriteHeight * row, SpriteWidth, SpriteHeight);
                    sprites.Add(rectangle);
                }
            }

            Sprites = sprites;
        }

        public void Draw(SpriteBatch spriteBatch, int sprite, Vector2 position)
        {
            var sourceRectangle = Sprites[sprite];
            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, int sprite, Vector2 position, float angle, Vector2 origin)
        {
            var sourceRectangle = Sprites[sprite];
            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, SpriteWidth, SpriteHeight);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, angle, origin, SpriteEffects.None, 0f);
        }

        public Sprite GetSprite(int index)
        {
            return new Sprite(index, this, SpriteWidth, SpriteHeight);
        }
    }

    class Sprite
    {
        private readonly int _spriteIndex;
        private readonly SpriteMap _map;
        public int Width { get; }
        public int Height { get; }

        public Sprite(int spriteIndex, SpriteMap map, int width, int height)
        {
            _spriteIndex = spriteIndex;
            _map = map;
            Width = width;
            Height = height;
        }

        public Sprite(Texture2D texture)
        {
            _spriteIndex = 0;
            _map = new SpriteMap(texture, 1, 1);
            Width = _map.SpriteWidth;
            Height = _map.SpriteHeight;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            _map.Draw(spriteBatch, _spriteIndex, position);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float angle, Vector2 origin)
        {
            _map.Draw(spriteBatch, _spriteIndex, position, angle, origin);
        }
    }
}
