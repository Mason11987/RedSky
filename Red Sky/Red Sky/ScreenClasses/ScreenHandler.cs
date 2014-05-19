using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    public enum ChangeType { Change, Pop, Push }

    public class ScreenHandler : DrawableGameComponent
    {
        Stack<GameScreen> gameScreens = new Stack<GameScreen>();
        public event EventHandler OnScreenChange;
        public List<Texture2D> IconsTexture;
        public Dictionary<string, SpriteFont> Fonts;
        public Texture2D CharacterSpriteTexture;

        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int drawOrder;
        RedSkyGame GameRef;

        public ScreenHandler(RedSkyGame game)
            : base(game)
        {
            GameRef = game;
            drawOrder = startDrawOrder;
            Initialize();
        }

        public GameScreen CurrentScreen
        {
            get { return gameScreens.Peek(); }
        }

        public override void Initialize()
        {
            base.Initialize();

            IconsTexture = new List<Texture2D>();
            for (int i = 0; i < 5; i++)
            {
                IconsTexture.Add(Game.Content.Load<Texture2D>("Icons/Icons" + (i + 1)));
            }


            Fonts = new Dictionary<string, SpriteFont>();

            Fonts.Add("myFont", Game.Content.Load<SpriteFont>("GUI/Fonts/myFont"));

            Fonts.Add("myFontBig", Game.Content.Load<SpriteFont>("GUI/Fonts/myFontBig"));
            Fonts.Add("myFontBigger", Game.Content.Load<SpriteFont>("GUI/Fonts/myFontBigger"));
            Fonts.Add("myFontBiggest", Game.Content.Load<SpriteFont>("GUI/Fonts/myFontBiggest"));
            Fonts.Add("myFontSmall", Game.Content.Load<SpriteFont>("GUI/Fonts/myFontSmall"));
            Fonts.Add("myFontSmaller", Game.Content.Load<SpriteFont>("GUI/Fonts/myFontSmaller"));
            Fonts.Add("myFontSmallest", Game.Content.Load<SpriteFont>("GUI/Fonts/myFontSmallest"));

            CharacterSpriteTexture = GameRef.Content.Load<Texture2D>(@"Character\Sprites");
        }


        public void PopScreen()
        {
            if (gameScreens.Count > 0)
            {
                RemoveScreen();

                if (OnScreenChange != null)
                    OnScreenChange(this, null);
            }
        }

        private void RemoveScreen()
        {           
            
            GameScreen oldScreen = gameScreens.Pop();
            oldScreen.Unload();
        }

        public void AddScreen(GameScreen newScreen)
        {
            gameScreens.Push(newScreen);
            
        }

        public void CloseScreen()
        {
            gameScreens.Pop();
        }

        public void ChangeScreen(GameScreen newScreen)
        {
            while (gameScreens.Count > 0)
                gameScreens.Pop();

            gameScreens.Push(newScreen);
        }

        internal void ChangeScreen<T>() where T : GameScreen
        {

            GameScreen screen = (T)Activator.CreateInstance(typeof(T), new object[] 
                { GameRef.GraphicsDevice.Viewport.Width, GameRef.GraphicsDevice.Viewport.Height, GameRef, this });
            screen.Initialize();

            if (gameScreens.Count > 0)
                gameScreens.Pop();

            gameScreens.Push(screen);
        }

        internal void AddScreen<T>() where T : GameScreen
        {
            GameScreen screen = (T)Activator.CreateInstance(typeof(T), new object[] { GameRef.GraphicsDevice.Viewport.Width, GameRef.GraphicsDevice.Viewport.Height, GameRef, this });
            screen.Initialize();
            gameScreens.Push(screen);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

        }

        internal void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {

            CurrentScreen.Draw(SpriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
