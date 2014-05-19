using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ModelClasses
{
    class Unit : DrawableGameComponent
    {
        private Character Character;


        private RedSkyGame GameRef;

        public Unit(RedSkyGame game)
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
