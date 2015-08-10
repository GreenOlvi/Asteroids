using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class Entity
    {
        protected AsteroidsGame GameInstance = AsteroidsGame.GetInstance();

        public Vector2 Position { get; protected set; }

        public virtual void LoadContent(ContentManager content) { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}