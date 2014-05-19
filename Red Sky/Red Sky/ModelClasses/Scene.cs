using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Red_Sky.ModelClasses;

namespace Red_Sky
{
    class Scene : DrawableGameComponent
    {
        public readonly List<Event> Events = new List<Event>();


        private RedSkyGame GameRef;

        public Scene(RedSkyGame game)
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
