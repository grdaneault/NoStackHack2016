using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoStackHack.ControlInput;
using NoStackHack.Rendering;
using NoStackHack.Utilities;
using System.Collections.Generic;
using NoStackHack.WorldMap;

namespace NoStackHack
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        Rectangle _screenSize;

        private RenderHelper _renderHelper;
        private List<Box> _boxes;
        private BackgroundImage _background;
        private List<Player> _players = new List<Player>();
        private World _world;

        public Game1()
        {
            _screenSize = new Rectangle(0, 0, 1920, 1080);
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;

            graphics.PreferredBackBufferWidth = _screenSize.Width;
            graphics.PreferredBackBufferHeight = _screenSize.Height;
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
            _renderHelper = new RenderHelper();
            _renderHelper.Init(GraphicsDevice);
            _boxes = BoxLoader.LoadFromFile("Content/test_world.boxes");
            _background = new BackgroundImage();
            _background.Init(GraphicsDevice, _screenSize);

            _world = WorldLoader.Load("Content/level1.map", Content);
            _world.Init(GraphicsDevice, _screenSize);
            _boxes = WorldLoader.GenerateHitboxes(_world);

            var player1 = new Player(PlayerIndex.One);
            var player2 = new Player(PlayerIndex.Two);
            _players.Add(player1);
            _players.Add(player2);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _background.LoadContent(Content);
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
            var inputHandler = new InputHandler();

            foreach(var player in _players)
            {
                var commands = inputHandler.HandleInput(player.PlayerIndex);
                foreach (var command in commands)
                {
                    command.Execute(player);
                }

                player.Update(gameTime, _boxes);



            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Navy);
            

            _world.DrawBackground();

            _background.Draw();

            
            _world.DrawForeground();

            _renderHelper.Batch.Begin();

            foreach (Box box in _boxes)
            {
                _renderHelper.DrawBox(box.Position, box.Size, new Color(Color.Red, 100));
            }


            foreach (var player in _players)
            {
                player.Render(_renderHelper);
                //_renderHelper.DrawBox(player.Box);
            }

            _renderHelper.Batch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
