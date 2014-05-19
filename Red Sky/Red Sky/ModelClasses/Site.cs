using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    public enum IconType { City, Field, Battle };

    class Site : DrawableGameComponent
    {
        public readonly List<Scene> Scenes = new List<Scene>();
        public readonly List<Site> Connections = new List<Site>();
        public Vector2 Location;
        public Region Region;


        private RedSkyGame GameRef;



        public IconType Icon
        {
            get { return IconType.City; }
        }
        

        public Site(RedSkyGame game)
            : base(game)
        {
            GameRef = game;
        }

        public Site(RedSkyGame game, Region region, Vector2 loc)
            : base(game)
        {
            GameRef = game;
            Region = region;
            Location = loc;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
