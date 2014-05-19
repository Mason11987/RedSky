using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Red_Sky.ControlClasses;
using Microsoft.Xna.Framework;
using Red_Sky.ModelClasses;
using Microsoft.Xna.Framework.Graphics;

namespace Red_Sky.ScreenClasses
{
    class MenuPartyScreen : MenuScreen
    {
        ObjectListControl characterList;

        public MenuPartyScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
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
            characterList.ItemHeight = 120;
            characterList.Bounds = new Rectangle(50, 100, 1500, 750);


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
                characterFrame =  thisCharacter.GetSpriteFrame(SpriteDirection.Front);
            }

            e.SpriteBatch.Draw(screenHandler.CharacterSpriteTexture, 
                new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + e.Bounds.Height / 2 - characterFrame.Height / 2, characterFrame.Width, characterFrame.Height), 
                characterFrame, Color.White);


            e.SpriteBatch.DrawString(characterList.Font, thisCharacter.Name, new Vector2(e.Bounds.Left + 40, e.Bounds.Top + e.Bounds.Height / 2 - 16), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            e.SpriteBatch.DrawString(characterList.Font, "HP:" + thisCharacter.HP + " / " + thisCharacter.MaxHP, new Vector2(e.Bounds.Left + 200, e.Bounds.Top + 4), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            thisCharacter.HP = thisCharacter.MaxHP / 2;

            sender.DrawRect(Color.DarkRed, new Rectangle(e.Bounds.Left + 200, e.Bounds.Top + 4 + 30, 200, 20), Color.Black);
            sender.DrawRect(Color.Red, new Rectangle(e.Bounds.Left + 201, e.Bounds.Top + 4 + 30 + 1, (int)(198.0f * ((float)thisCharacter.HP / thisCharacter.MaxHP)), 19));

            e.SpriteBatch.DrawString(characterList.Font, "Level:" + thisCharacter.Level, new Vector2(e.Bounds.Left + 200, e.Bounds.Top + 4 + 50), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            thisCharacter.Experience = thisCharacter.ExpToNextLevel / 3;

            sender.DrawRect(Color.DarkGreen, new Rectangle(e.Bounds.Left + 200, e.Bounds.Top + 4 + 80, 200, 20), Color.Black);
            sender.DrawRect(Color.Green, new Rectangle(e.Bounds.Left + 201, e.Bounds.Top + 4 + 80 + 1, (int)(198.0f * ((float)thisCharacter.Experience / thisCharacter.ExpToNextLevel)), 19));
        }
    }
}
