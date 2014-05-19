using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    class Region : DrawableGameComponent
    {
        public readonly List<Site> Sites = new List<Site>();

        private RedSkyGame GameRef;

        public Region(RedSkyGame game)
            : base(game)
        {
            GameRef = game;
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
