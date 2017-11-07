﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoapHero
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /////////////////////////////////////// begin /////////////////////////////////
        enum GameState
        {
            MainMenu,
            Options,
            Playing,
        }

        GameState CurrentGameState = GameState.MainMenu;

        //screen adjustment
        int screenWith = 800; int screenHight = 600;

        cButton btnPlay;
        cButton btnOptions;
        //////////////////////////////////////// end /////////////////////////////////
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //////////////////////////////// begin ////////////////////////////////////////
            ///screen Stuff
            graphics.PreferredBackBufferWidth = screenWith;
            graphics.PreferredBackBufferHeight = screenHight;

            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            btnPlay = new cButton(Content.Load<Texture2D>("playBtn"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(300, 300));

            btnOptions = new cButton(Content.Load<Texture2D>("optionsBtn"), graphics.GraphicsDevice);
            btnOptions.setPosition(new Vector2(300, 340));
            //////////////////////////////////// end ////////////////////////////////////
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            ////////////////////////////////////// begin ///////////////////////////////////
            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    
//                    if (btnOptions.isClicked == true) CurrentGameState = GameState.Options;
                    btnOptions.Update(mouse);

                    break;
                case GameState.Playing:
                    break;
                case GameState.Options:
                    break;
            }
            /////////////////////////////////////// end ////////////////////////////////////

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            /////////////////////////////////////// begin ////////////////////////////////////
            spriteBatch.Begin();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, screenWith, screenHight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnOptions.Draw(spriteBatch);

                    break;
                case GameState.Playing:
                    break;
                case GameState.Options:
                    break;
            }
            spriteBatch.End();
            /////////////////////////////////////// end ////////////////////////////////////

            base.Draw(gameTime);
        }
    }
}
