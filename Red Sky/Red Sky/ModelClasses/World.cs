using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nuclex.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Red_Sky.ScreenClasses;

namespace Red_Sky.ModelClasses
{
    internal class World : DrawableGameComponent
    {
        public readonly List<Flag> Flags = new List<Flag>();
        public readonly List<Region> Regions = new List<Region>();
        public Party Player;

        VertexPositionColor[] vertices;
        
        private new RedSkyGame Game;

        public World(RedSkyGame game)
            : base(game)
        {

            Game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MapScreen Screen)
        {
            base.Draw(gameTime);

            //spriteBatch.Draw(boxTexture, new Rectangle(x++,0, 100, 100), Color.White);

            Game.basicEffect.CurrentTechnique.Passes[0].Apply();
            vertices = new VertexPositionColor[2];

            vertices[0].Color = Color.Black;
            vertices[1].Color = Color.Black;

            foreach (Region region in Regions)
            {
                foreach (Site site in region.Sites)
                {
                    vertices[0].Position = new Vector3(site.Location.X, site.Location.Y, 0);
                    foreach (Site conSite in site.Connections)
                    {
                        vertices[1].Position = new Vector3(conSite.Location.X, conSite.Location.Y, 0);
                        Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, 1);
                    }
                }

                foreach (Site site in region.Sites)
                {
                    switch (site.Icon)
                    {
                        case IconType.City:
                            spriteBatch.Draw(Screen.SiteIconsTexture, new Rectangle((int)site.Location.X - 16, (int)site.Location.Y - 16, 32, 32), new Rectangle(0, 0, 32, 32), Color.White);
                            break;
                        case IconType.Field:
                            break;
                        case IconType.Battle:
                            break;
                        default:
                            break;
                    }
                }
            }

            spriteBatch.Draw(Game.screenHandler.CharacterSpriteTexture, new Rectangle((int)Player.Site.Location.X - 16, (int)Player.Site.Location.Y - 32, 32, 32), Player.Characters[0].GetAnimatedSpriteFrame(SpriteDirection.Front), Color.White);



 
        }

        public override void Update(GameTime gameTime)
        {

            foreach (Character character in Player.Characters)
            {
                if (character.Enabled)
                    character.Update(gameTime);
            } 
            base.Update(gameTime);
        }


        internal static World StartNew(RedSkyGame game)
        {
            
            World World = InitializeNewWorld(game);

            return World;
        }

        private static World InitializeNewWorld(RedSkyGame game)
        {
            World World = new World(game);



            Party PlayerParty = new Party(game);
            for (int i = 0; i < 4; i++)
            {
                Character MainCharacter = Character.Initalize<Human>(game);
                MainCharacter.Enabled = true;
                MainCharacter.SpriteLocation = new Vector2(i, 0);
                MainCharacter.Name = "Test " + i;
                PlayerParty.Join(MainCharacter);
            }

            PlayerParty.Items = new Dictionary<Item, int>();
            PlayerParty.Items.Add(game.Items["Potion"], 9);
            PlayerParty.Items.Add(game.Items["Pheonix Down"], 177);
            PlayerParty.Items.Add(game.Items["Ether"], 1);
            PlayerParty.Items.Add(game.Items["Mega Potion"], 42);
            PlayerParty.Items.Add(game.Items["Elixer"], 7);
            PlayerParty.Items.Add(game.Items["Remedy"], 10);
            PlayerParty.Items.Add(game.Items["Hi-Potion"], 1);
            PlayerParty.Items.Add(game.Items["Sword"], 3);
            PlayerParty.Items.Add(game.Items["Axe"], 1);
            PlayerParty.Items.Add(game.Items["Fire Item"], 5);
            PlayerParty.Items.Add(game.Items["Shirt"], 2);
            PlayerParty.Items.Add(game.Items["Helmet"], 3);
            PlayerParty.Items.Add(game.Items["Gloves"], 1);

            World.Player = PlayerParty;



            Region MainRegion = new Region(game);
            World.Regions.Add(MainRegion);

            MainRegion.Sites.Add(new Site(game, MainRegion, new Vector2(300, 300)));
            MainRegion.Sites.Add(new Site(game, MainRegion, new Vector2(400, 350)));
            MainRegion.Sites.Add(new Site(game, MainRegion, new Vector2(210, 300)));
            MainRegion.Sites.Add(new Site(game, MainRegion, new Vector2(320, 550)));
            MainRegion.Sites.Add(new Site(game, MainRegion, new Vector2(270, 150)));
            MainRegion.Sites[0].Connections.Add(MainRegion.Sites[1]);
            MainRegion.Sites[0].Connections.Add(MainRegion.Sites[2]);
            MainRegion.Sites[0].Connections.Add(MainRegion.Sites[3]);

            MainRegion.Sites[1].Connections.Add(MainRegion.Sites[3]);
            MainRegion.Sites[2].Connections.Add(MainRegion.Sites[4]);

            foreach (var site in MainRegion.Sites)
            {
                site.Visible = true;
            }

            PlayerParty.Site = MainRegion.Sites[0];

            return World;

        }

        internal void MouseButtonPressed(MouseButtons buttons, MouseState mouseState)
        {
            if (buttons == MouseButtons.Left)
            {
                Site site = getSiteAt(new Vector2(mouseState.X, mouseState.Y));
                if (site != null)
                {
                    if (Player.Site.Connections.Contains(site) || site.Connections.Contains(Player.Site))
                    {
                        MovePlayerTo(site);
                    }
                }
            }
        }

        private void MovePlayerTo(Site site)
        {
            Player.Site = site;
        }

        private Site getSiteAt(Vector2 loc)
        {
            Site siteAt = null;

            foreach (Region region in Regions)
            {
                foreach (Site site in region.Sites)
                {
                    if (loc.X >= site.Location.X - 16 && loc.X <= site.Location.X + 16 &&
                            loc.Y >= site.Location.Y - 16 && loc.Y <= site.Location.Y + 16)
                        return site;
                }
            }

            return siteAt;
        }
    }
}
