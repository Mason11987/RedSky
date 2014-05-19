using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    class Icon : DrawableGameComponent
    {
        public int Page;
        public Rectangle PageRect;
        public int hPage;
        public Rectangle hPageRect;
        public Rectangle ScreenRect;
        public Vector2 ScreenLoc;

        public Rectangle DrawRect
        {
            get { return new Rectangle(ScreenRect.X + (int)ScreenLoc.X, ScreenRect.Y + (int)ScreenLoc.Y, ScreenRect.Width, ScreenRect.Height);; }
        }
        

        public Icon(RedSkyGame game, int page, Vector2 pageloc, Rectangle screenrect) 
            : base(game)
        {
            Page = page;
            PageRect = new Rectangle((int)pageloc.X * 24, (int)pageloc.Y * 24, 24, 24);
            ScreenRect = screenrect;
            hPage = 0;
            hPageRect = Rectangle.Empty;
        }

        public Icon(RedSkyGame game, int page, Vector2 pageloc, int hpage, Vector2 hpageloc, Rectangle screenrect)
            : base(game)
        {
            Page = page;
            PageRect = new Rectangle((int)pageloc.X * 24, (int)pageloc.Y * 24, 24, 24);
            hPage = hpage;
            hPageRect = new Rectangle((int)hpageloc.X * 24, (int)hpageloc.Y * 24, 24, 24);
            ScreenRect = screenrect;
        }

        internal void Draw(SpriteBatch SpriteBatch, GameTime gameTime, Vector2 MouseLoc, List<Texture2D> IconsTexture)
        {
            var drawRect = DrawRect;
            if (MouseLoc.X >= drawRect.Left && MouseLoc.X < drawRect.Right &&
                MouseLoc.Y >= drawRect.Top && MouseLoc.Y < drawRect.Bottom)
                SpriteBatch.Draw(IconsTexture[hPage], drawRect, hPageRect, Color.White);
            else
                SpriteBatch.Draw(IconsTexture[Page], drawRect, PageRect, Color.White);
        }

        internal void Draw(SpriteBatch SpriteBatch, GameTime gameTime, Vector2 MouseLoc, List<Texture2D> IconsTexture, Vector2 offsetPos)
        {

        }

        internal bool Contains(Microsoft.Xna.Framework.Input.MouseState mouseState)
        {
            return DrawRect.Contains(new Point(mouseState.X, mouseState.Y));
        }
    }

}
