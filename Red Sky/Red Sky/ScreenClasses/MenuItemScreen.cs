using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Red_Sky.ControlClasses;
using Red_Sky.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface;

namespace Red_Sky.ScreenClasses
{
    class MenuItemScreen : MenuScreen
    {

        ObjectListControl itemList;
        ObjectListControl characterList;
        ButtonControl useButton;

        public MenuItemScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
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
            useButton = new ButtonControl();
            useButton.Text = "Use";
            useButton.Bounds = new UniRectangle(
              new UniScalar(0.0f, 1470.0f), new UniScalar(0.0f, 770.0f), 80, 80
            );
            useButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                useButtonClicked();
            };
            useButton.Enabled = false;
            this.Desktop.Children.Add(useButton);


            // Item List
            itemList = new ObjectListControl(Game, this);
            itemList.ItemHeight = 40;
            itemList.Bounds = new Rectangle(50, 100, 400, 650);
        
            PopulateItems();

            this.Components.Add(itemList);

            itemList.DrawItem += new DrawItemEventHandler(itemList_DrawItem);
            itemList.ItemSelected += new ItemSelectedEventHandler(itemList_ItemSelected);

            itemList.Select(0);

            // Character List
            characterList = new ObjectListControl(Game, this);
            characterList.ItemHeight = 40;
            characterList.Bounds = new Rectangle(470, 100, 1080, 650);


            foreach (Character character in Party.Characters)
            {
                characterList.AddItem(character, false);
            }
            characterList.Visible = false;

            this.Components.Add(characterList);

            characterList.DrawItem += new DrawItemEventHandler(characterList_DrawItem);
            characterList.ItemSelected += new ItemSelectedEventHandler(characterList_ItemSelected);

        }


        public override void Unload()
        {
            base.Unload();
            itemList.DrawItem -= new DrawItemEventHandler(itemList_DrawItem);
            itemList.ItemSelected -= new ItemSelectedEventHandler(itemList_ItemSelected);
            characterList.DrawItem -= new DrawItemEventHandler(characterList_DrawItem);
            characterList.ItemSelected -= new ItemSelectedEventHandler(characterList_ItemSelected);
        }


        private void useButtonClicked()
        {
            Item thisItem = (Item)itemList.SelectedItem;

            if (thisItem.Targets == ItemTargets.Character)
            {
                Character thisCharacter = (Character)characterList.SelectedItem;

                Party.UseItem(thisItem, thisCharacter);
            }
            else if (thisItem.Targets == ItemTargets.Party)
            {
                Party.UseItem(thisItem, Party);
            }
            //useButton.Enabled = false;
            this.FocusedControl = null;

        }

        private void PopulateItems()
        {
            itemList.Clear();
            itemList.Selected.Clear();
            foreach (Item item in Game.World.Player.Items.Keys)
            {
                if (Game.World.Player.Items[item] > 0)
                {
                    itemList.AddItem(item, false);
                }
            }
        }

        private void itemList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Console.WriteLine("Selected " + e.Index);
            Item thisItem = (Item)itemList.Items[e.Index];
            switch (thisItem.Usage)
            {
                case ItemUse.Wearable:
                    //DrawEquipableItem(SpriteBatch, gameTime, thisItem);
                    break;
                case ItemUse.Usable:
                    if (characterList != null)
                        characterList.Select(0);
                    break;
                case ItemUse.BattleUsable:
                    break;
                default:
                    break;
            }
        }

        private void itemList_DrawItem(ObjectListControl sender, DrawItemEventArgs e)
        {
            Item thisItem = (Item)itemList.Items[e.Index];

            //Rectangle characterFrame;
            if (itemList.Selected[e.Index])
            {
                sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds);

                //characterFrame = thisCharacter.GetAnimatedSpriteFrame(SpriteDirection.Front);
            }
            else
            {
                //characterFrame = thisCharacter.GetSpriteFrame(SpriteDirection.Front);
            }

            //e.SpriteBatch.Draw(Game.World.CharacterSpriteTexture,
            //    new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + e.Bounds.Height / 2 - characterFrame.Height / 2, characterFrame.Width, characterFrame.Height),
            //    characterFrame, Color.White);


            e.SpriteBatch.DrawString(itemList.Font, thisItem.Name, new Vector2(e.Bounds.Left + 40, e.Bounds.Top + 4), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            e.SpriteBatch.DrawString(itemList.Font, Game.World.Player.Items[thisItem].ToString().PadLeft(3), new Vector2(e.Bounds.Right - 50, e.Bounds.Top + 4), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }

        internal override void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {
            Item thisItem = (Item)itemList.SelectedItem;

            if (thisItem == null)
                return;

            switch (thisItem.Usage)
            {
                case ItemUse.Wearable:
                    DrawEquipableItem(SpriteBatch, gameTime, thisItem);
                    break;
                case ItemUse.Usable:
                    DrawUsableItem(SpriteBatch, gameTime, thisItem);
                    break;
                case ItemUse.BattleUsable:
                    break;
                default:
                    break;
            }

            Rectangle DescriptionFrame = new Rectangle(50, 770, 1400, 80);

            DrawRect(Color.White, DescriptionFrame, Color.Black);

            if (itemList.HoverIndex == -1)
                SpriteBatch.DrawString(Font, thisItem.Description, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                    0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            else
                SpriteBatch.DrawString(Font, ((Item)itemList.Items[itemList.HoverIndex]).Description, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                    0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            base.Draw(SpriteBatch, gameTime);
        }

        private void DrawUsableItem(SpriteBatch SpriteBatch, GameTime gameTime, Item thisItem)
        {
            useButton.Enabled = Party.Items[thisItem] > 0; ;
            characterList.Visible = true;
            //throw new NotImplementedException();
        }

        private void characterList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Console.WriteLine("Selected " + e.Index);

        }

        private void characterList_DrawItem(ObjectListControl sender, DrawItemEventArgs e)
        {
            Character thisCharacter = (Character)characterList.Items[e.Index];
            Item thisItem = (Item)itemList.SelectedItem;

            Rectangle characterFrame;
            if (characterList.Selected[e.Index] || thisItem.Targets == ItemTargets.Party)
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


            e.SpriteBatch.DrawString(characterList.Font, thisCharacter.Name, new Vector2(e.Bounds.Left + 40, e.Bounds.Top + e.Bounds.Height / 2 - 16), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            e.SpriteBatch.DrawString(characterList.Font, thisItem.UseStatStringReference(thisCharacter), new Vector2(e.Bounds.Left + 200, e.Bounds.Top + 4), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);


        }

        private void DrawEquipableItem(SpriteBatch SpriteBatch, GameTime gameTime, Item thisItem)
        {
            characterList.Visible = false;
            useButton.Enabled = false;
            //throw new NotImplementedException();
        }
    }
}
