using Asteroids.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.View
{
    public class PlayerDrawer : IEntityDrawer<Player>
    {
        public EntityType Type => EntityType.Player;

        private const int SPRITE_NORMAL = 0;
        private const int SPRITE_ACCELERATE = 1;

        private Sprite[] _sprites;
        private Vector2 RotationOrigin { get; set; }

        public void LoadContent(ContentManager contentManager)
        {
            var playerSprite = contentManager.Load<Texture2D>(@"player");
            var playerMap = new SpriteMap(playerSprite, 2, 1);
            _sprites = new Sprite[2];
            _sprites[SPRITE_NORMAL] = playerMap.GetSprite(0);
            _sprites[SPRITE_ACCELERATE] = playerMap.GetSprite(1);
            RotationOrigin = new Vector2(10, 15);
        }

        public void Draw(SpriteBatch spriteBatch, IEntity entity)
        {
            Draw(spriteBatch, (Player) entity);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            var spriteIndex = player.IsAccelerating ? SPRITE_ACCELERATE : SPRITE_NORMAL;
            _sprites[spriteIndex].Draw(spriteBatch, player.Position, player.Angle, RotationOrigin);
        }
    }
}
