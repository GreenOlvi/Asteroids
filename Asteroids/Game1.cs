#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Asteroids.View;
#endregion

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        private SpriteFont font;
        private Player player;

        private bool DebugOutput = true;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void NewGame()
        {
            player = new Player(new Vector2(ScreenWidth / 2, ScreenHeight / 2));
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

            NewGame();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerSprite = Content.Load<Texture2D>(@"player");
            var playerMap = new SpriteMap(playerSprite, 2, 1);
            Player.Sprites[Player.SPRITE_NORMAL] = playerMap.getSprite(0);
            Player.Sprites[Player.SPRITE_ACCELERATE] = playerMap.getSprite(1);

            font = Content.Load<SpriteFont>(@"RetroFontSmall");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                NewGame();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.BigButton == ButtonState.Pressed)
            {
                DebugOutput = !DebugOutput;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            {
                player.RotateLeft();
            }
            else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            {
                player.RotateRight(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * -1.0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.RotateRight();
            }
            else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
            {
                player.RotateRight(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                player.isAccelerating = true;
            }
            else if (player.isAccelerating)
            {
                player.isAccelerating = false;
            }

            player.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            PrintDebug(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void PrintDebug(SpriteBatch spriteBatch)
        {
            var debug = new List<string>();

            if (DebugOutput)
            {
                debug.Add(String.Format(@"X {0:0}", player.Position.X));
                debug.Add(String.Format(@"Y {0:0}", player.Position.Y));
                debug.Add(String.Format(@"a {0:0.00}", player.Angle));
                debug.Add(String.Format(@"pad {0}", GamePad.GetState(PlayerIndex.One).IsConnected));
                debug.Add(String.Format(@"ls x {0:0.00}", GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X));

                var i = 0;
                foreach (var s in debug)
                {
                    spriteBatch.DrawString(font, s, new Vector2(0, i * 30), Color.White);
                    i++;
                }

                debug.Clear();
            }
        }
    }
}
