using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Red_Sky.ControlClasses;
using Red_Sky.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    class MenuSkillScreen : MenuScreen
    {

        ObjectListControl characterList;


        public MenuSkillScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
            : base(width, height, game, manager)
        {

        }

        internal override void LoadContent()
        {
            base.LoadContent();

            createControls();
        }

        private void createControls()
        {

            // Character List
            characterList = new ObjectListControl(Game, this);
            characterList.ItemHeight = 40;
            characterList.Bounds = new Rectangle(50, 100, 400, 750);

            foreach (Character character in Party.Characters)
            {
                characterList.AddItem(character, false);
            }

            this.Components.Add(characterList);

            characterList.DrawItem += new DrawItemEventHandler(characterList_DrawItem);
            characterList.ItemSelected += new ItemSelectedEventHandler(characterList_ItemSelected);

            characterList.Select(0);
        }


        public override void Unload()
        {
            base.Unload();
            characterList.DrawItem -= new DrawItemEventHandler(characterList_DrawItem);
            characterList.ItemSelected -= new ItemSelectedEventHandler(characterList_ItemSelected);
        }

        private void characterList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Console.WriteLine("Selected " + e.Index);

        }

        private void characterList_DrawItem(ObjectListControl sender, DrawItemEventArgs e)
        {
            Character thisCharacter = (Character)characterList.Items[e.Index];

            Rectangle characterFrame;
            if (characterList.Selected[e.Index])
            {
                sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds);

                characterFrame = thisCharacter.GetAnimatedSpriteFrame(SpriteDirection.Front);
            }
            else
            {
                characterFrame = thisCharacter.GetSpriteFrame(SpriteDirection.Front);
            }

            e.SpriteBatch.Draw(screenHandler.CharacterSpriteTexture,
                new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + e.Bounds.Height / 2 - characterFrame.Height / 2, characterFrame.Width, characterFrame.Height),
                characterFrame, Color.White);


            e.SpriteBatch.DrawString(characterList.Font, thisCharacter.Name, new Vector2(e.Bounds.Left + 40, e.Bounds.Top + 4), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }

        internal override void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {

            Character thisCharacter = (Character)characterList.SelectedItem;

            if (thisCharacter == null)
                return;

            Rectangle StatusFrame = new Rectangle(470, 100, 1080, 750);

            DrawRect(Color.White, StatusFrame, Color.Black);

            Rectangle characterFrame = thisCharacter.GetAnimatedSpriteFrame(SpriteDirection.Front);

            SpriteBatch.Draw(screenHandler.CharacterSpriteTexture,
                new Rectangle(StatusFrame.Left + 10, StatusFrame.Top + 10, characterFrame.Width, characterFrame.Height),
                characterFrame, Color.White);

            SpriteBatch.DrawString(Font, thisCharacter.Name, new Vector2(StatusFrame.Left + 60, StatusFrame.Top + 8), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            base.Draw(SpriteBatch, gameTime);

        }
    }
}
