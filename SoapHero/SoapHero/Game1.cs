using Microsoft.Xna.Framework;
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

        #region declarations
        GameState CurrentGameState = GameState.MainMenu;

        //screen adjustment
        int screenWidth = 1280; int screenHeight = 720;

        cButton btnPlay;
        cButton btnOptions;
        cButton btnQuit;
        
        LevelManager levelManager;
        #endregion

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
            ///screen Stuff
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            levelManager = new LevelManager(this.graphics.GraphicsDevice);
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
            IsMouseVisible = true;
            btnPlay = new cButton(Content.Load<Texture2D>("playBtn"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(screenWidth / 2 - btnPlay.size.X / 2, screenHeight / 2 - btnPlay.size.Y / 2));

            btnOptions = new cButton(Content.Load<Texture2D>("optionsBtn"), graphics.GraphicsDevice);
            btnOptions.setPosition(new Vector2(screenWidth / 2 - btnPlay.size.X / 2, screenHeight / 2 - btnPlay.size.Y / 2 + 40));

            btnQuit = new cButton(Content.Load<Texture2D>("quitBtn"), graphics.GraphicsDevice);
            btnQuit.setPosition(new Vector2(screenWidth / 2 - btnPlay.size.X / 2, screenHeight / 2 - btnPlay.size.Y / 2 + 80));
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

            #region logic
            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) 
                    {
                        levelManager.InitializeWorld(Content, graphics);
                        CurrentGameState = GameState.Playing;
                    }
                    //if (btnOptions.isClicked == true) CurrentGameState = GameState.Options;
                    if (btnQuit.isClicked == true) Exit();

                    btnPlay.Update(mouse);                  
                    btnOptions.Update(mouse);
                    btnQuit.Update(mouse);

                    break;
                case GameState.Playing:
                    levelManager.Update(gameTime, graphics);
                    break;
                case GameState.Options:
                    break;
            }
            #endregion

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
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnOptions.Draw(spriteBatch);
                    btnQuit.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    //levelManager.Draw(CurrentWorldState, cameraRect, spriteBatch);
                    levelManager.Draw(GraphicsDevice, spriteBatch);
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
