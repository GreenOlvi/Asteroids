using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Model
{
    class Entity
    {
        protected AsteroidsGame GameInstance = AsteroidsGame.Instance;
        protected InputManager inputManager = InputManager.Instance;
        protected EntityManager entityManager = EntityManager.Instance;

        public Vector2 Position { get; protected set; }
        public bool Destroyed { get; protected set; }

        public static void LoadContent(ContentManager content) { }
        public static void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}