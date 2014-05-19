using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    class MapScreen : GameScreen
    {
        private Icon EnterIcon;
        private Icon MenuIcon;
        private Icon ShopIcon;
        private Icon DataIcon;
        private double LastUpdate;

        private Vector2[,] textures = new Vector2[20,20];
        private int[,] sheets = new int[20, 20];
        public Texture2D SiteIconsTexture;

        


        public MapScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
            : base(width, height, game, manager)
        {

        }

        internal override void Initialize()
        {
            base.Initialize();
            

            Components.Add(Game.World);

            SiteIconsTexture = Game.Content.Load<Texture2D>(@"Icons\Site.Icons");

            EnterIcon = new Icon(Game, 0, new Vector2(0, 3), 0, new Vector2(0, 20), new Rectangle(0, 0, 24, 24));
            MenuIcon = new Icon(Game, 0, new Vector2(5, 8), 0, new Vector2(5, 25), new Rectangle(24, 0, 24, 24));
            ShopIcon = new Icon(Game, 0, new Vector2(3, 9), 0, new Vector2(3, 26), new Rectangle(48, 0, 24, 24));
            DataIcon = new Icon(Game, 0, new Vector2(10, 9), 0, new Vector2(10, 26), new Rectangle(72, 0, 24, 24));


            UpdateIconLocations();
        }

        public override void MouseButtonReleased(Nuclex.Input.MouseButtons buttons)
        {
            base.MouseButtonReleased(buttons);

            if (buttons == Nuclex.Input.MouseButtons.Left && screenHandler.CurrentScreen == this)
            {
                var mouseState = Game.input.GetMouse().GetState();
                Game.World.MouseButtonPressed(buttons, mouseState);
                if (EnterIcon.Contains(mouseState))
                    ;
                else if (MenuIcon.Contains(mouseState))
                    screenHandler.AddScreen<MenuPartyScreen>();
                else if (ShopIcon.Contains(mouseState))
                    ;
                else if (DataIcon.Contains(mouseState))
                    ;
                buttons = 0;
            }
        }

        public override void MouseMoved(float x, float y)
        {
            MouseLoc = new Vector2(x, y);
        } 

        internal override void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {
            Game.World.Draw(SpriteBatch, gameTime, this);

            EnterIcon.Draw(SpriteBatch, gameTime, MouseLoc, screenHandler.IconsTexture);
            MenuIcon.Draw(SpriteBatch, gameTime, MouseLoc, screenHandler.IconsTexture);
            ShopIcon.Draw(SpriteBatch, gameTime, MouseLoc, screenHandler.IconsTexture);
            DataIcon.Draw(SpriteBatch, gameTime, MouseLoc, screenHandler.IconsTexture);

        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateIconLocations();
        }

        private void UpdateIconLocations()
        {
            EnterIcon.ScreenLoc = Game.World.Player.Site.Location + new Vector2(-48, 12);
            MenuIcon.ScreenLoc = Game.World.Player.Site.Location + new Vector2(-48, 12);
            ShopIcon.ScreenLoc = Game.World.Player.Site.Location + new Vector2(-48, 12);
            DataIcon.ScreenLoc = Game.World.Player.Site.Location + new Vector2(-48, 12);
        }
    }
}
