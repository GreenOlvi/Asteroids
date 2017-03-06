using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Asteroids.Model;
using Asteroids.View;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AsteroidsGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public Rectangle Screen { get; private set; }

        private readonly InputManager _inputManager = InputManager.Instance;
        private readonly EntityManager _entityManager = new EntityManager();
        private readonly EntityDrawerManager _entityDrawerManager;
        public CompoundGravity Gravity { get; }

        private SpriteFont _font;
        private Player _player;

        public bool DebugOutput { get; set; } = true;

        private static AsteroidsGame _instance;
        public static AsteroidsGame Instance {
            get
            {
                if (_instance == null)
                    _instance = new AsteroidsGame();
                return _instance;
            }
        }

        private AsteroidsGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _entityDrawerManager = new EntityDrawerManager(new PlayerDrawer(), new AsteroidDrawer(), new LaserProjectileDrawer());
            Gravity = new CompoundGravity();
        }

        private void NewGame()
        {
            _entityManager.Clear();

            _player = new Player(new Vector2(ScreenWidth / 2, ScreenHeight / 2));
            AddEntity(_player);

            var asteroid = Asteroid.GenerateRandom(new Vector2((ScreenWidth/2) + 200, ScreenHeight/2));
            _entityManager.Add(asteroid);

            var singularity = new Singularity(asteroid.Position, 1000);
            AddEntity(singularity);
            Gravity.AddSource(singularity);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            Screen = new Rectangle(0, 0, ScreenWidth, ScreenHeight);

            NewGame();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            View.Primitives.Device = GraphicsDevice;

            _entityDrawerManager.LoadContent(Content);

            _font = Content.Load<SpriteFont>(@"RetroFontSmall");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _inputManager.Update();

            if (_inputManager.KeyPressed(Keys.Escape))
                Exit();

            if (_inputManager.KeyPressed(Keys.Enter) ||
                _inputManager.GamePadButtonPressed(PlayerIndex.One, Buttons.Start))
                NewGame();

            if (_inputManager.GamePadButtonPressed(PlayerIndex.One, Buttons.Y))
                DebugOutput = !DebugOutput;

            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _entityDrawerManager.DrawEntities(_spriteBatch, _entityManager.Entities);
            PrintDebug(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void PrintDebug(SpriteBatch spriteBatch)
        {
            var debug = new List<string>();

            if (DebugOutput)
            {
                debug.Add(String.Format(@"X {0:0}", _player.Position.X));
                debug.Add(String.Format(@"Y {0:0}", _player.Position.Y));
                debug.Add(String.Format(@"a {0:0.00}", _player.Angle));
                debug.Add(String.Format(@"pad {0}", GamePad.GetCapabilities(PlayerIndex.One).HasLeftVibrationMotor));
                debug.Add(String.Format(@"ls x {0:0.00}", GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X));
                debug.Add(String.Format(@"entities {0}", _entityManager.EntityCount));

                var i = 0;
                foreach (var s in debug)
                {
                    spriteBatch.DrawString(_font, s, new Vector2(0, i * 30), Color.White);
                    i++;
                }

                debug.Clear();
            }
        }

        public void AddEntity(IEntity entity)
        {
            _entityManager.Add(entity);
        }
    }
}
