using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface.Visuals.Flat;
using Nuclex.Input;
using Nuclex.UserInterface;
using Red_Sky.ScreenClasses;



using Nuclex.Graphics.SpecialEffects.Particles.HighLevel;
using Nuclex.Graphics.SpecialEffects.Particles;
using Red_Sky.ModelClasses;

namespace Red_Sky
{

    public class RedSkyGame : Microsoft.Xna.Framework.Game
    {

        private GraphicsDeviceManager graphics;
        private GuiManager gui;
        public InputManager input;
        public ScreenHandler screenHandler;

        public SpriteBatch SpriteBatch;
        public BasicEffect basicEffect;
        public Texture2D boxTexture;
        public Texture2D tilesTexture;
        
        internal World World;
        public Random rnd = new Random();

        public Items Items = new Items();

        public RedSkyGame()
        {

            this.graphics = new GraphicsDeviceManager(this);
            this.input = new InputManager(Services, Window.Handle);
            this.gui = new GuiManager(Services);

            this.graphics.PreferredBackBufferHeight = 900;
            this.graphics.PreferredBackBufferWidth = 1600;

            this.graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            // Automatically query the input devices once per update
            Components.Add(this.input);

            // You can either add the GUI to the Components collection to have it render
            // automatically, or you can call the GuiManager's Draw() method yourself
            // at the appropriate place if you need more control.
            Components.Add(this.gui);
            this.gui.DrawOrder = 1000;


            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

#if DEBUG
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                User32.SetWindowPos((uint)this.Window.Handle, 0, 1700, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, 0);
#else 
            this.graphics.IsFullScreen = true;
            this.graphics.ApplyChanges();
#endif
            screenHandler = new ScreenHandler(this);

            screenHandler.ChangeScreen<StartScreen>();

            this.gui.Screen = screenHandler.CurrentScreen;

            screenHandler.Initialize();

            Items.Initialize(this);
        }


        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, GraphicsDevice.Viewport.Width,     // left, right
                GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                         // near, far plane

            // TODO: Load any content
        }
        protected override void UnloadContent()
        {
            Content.Unload();

            // TODO: Unload any content
        }

        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            screenHandler.Update(gameTime);
            this.gui.Screen = screenHandler.CurrentScreen;
            base.Update(gameTime);
        }


        

        /// <summary>This is called when the game should draw itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();

            graphics.GraphicsDevice.Clear(Color.LightSlateGray);

            base.Draw(gameTime);

            //Level.Draw(GameRef.SpriteBatch, gameTime);

            screenHandler.Draw(SpriteBatch, gameTime); //Draws XNA components of the current screen


            // TODO: Add your drawing code here
            base.Draw(gameTime);

            this.SpriteBatch.End();


        }


        

    }

} 
