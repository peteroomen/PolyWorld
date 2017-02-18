using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PolyWorld.Terrain;
using System;

namespace PolyWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PolyWorldGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private BasicEffect baseEffect;

        private Land land;

        Camera camera;

        private bool toggleFullscreen = false;

        public PolyWorldGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.HardwareModeSwitch = false;
            graphics.PreferMultiSampling = true;


            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

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
            baseEffect = new BasicEffect(graphics.GraphicsDevice);
            land = new Land();
            camera = new Camera(graphics.GraphicsDevice);

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

            // TODO: use this.Content to load your game content here
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
            KeyboardState keyboardstatecurrent = Keyboard.GetState();
            if (keyboardstatecurrent.IsKeyDown(Keys.Escape)) {
                Exit();
            }

            if (keyboardstatecurrent.IsKeyDown(Keys.F11))
            {
                toggleFullscreen = true;
            }
            else if (toggleFullscreen == true)
            {
                if (graphics.IsFullScreen)
                {
                    graphics.IsFullScreen = false;
                    graphics.PreferredBackBufferWidth = 1280;
                    graphics.PreferredBackBufferHeight = 720;
                }
                else
                {
                    graphics.IsFullScreen = true;
                    graphics.PreferredBackBufferWidth = 1920;
                    graphics.PreferredBackBufferHeight = 1080;
                }
                graphics.ApplyChanges();
                toggleFullscreen = false;
            }

            camera.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // New camera code
            baseEffect.View = camera.ViewMatrix;
            baseEffect.Projection = camera.ProjectionMatrix;

            baseEffect.VertexColorEnabled = true;
            
            baseEffect.LightingEnabled = true;
            baseEffect.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f); // a white light
            baseEffect.DirectionalLight0.Direction = new Vector3(0f, -0.5f, -0.5f);  // coming along the x-axis
            baseEffect.DirectionalLight0.SpecularColor = new Vector3(0, 0, 0); // with green highlights

            baseEffect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);

            land.Draw(graphics, baseEffect);

            base.Draw(gameTime);
        }
    }
}
