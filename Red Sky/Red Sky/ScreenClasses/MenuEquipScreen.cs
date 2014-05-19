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
    class MenuEquipScreen : MenuScreen
    {

        ObjectListControl characterList;
        ObjectListControl partList;
        ObjectListControl holdingList;
        ObjectListControl wearingList;


        public MenuEquipScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
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

            // BodyPart List
            partList = new ObjectListControl(Game, this);
            partList.Bounds = new Rectangle(910, 140, 200, 300);
            partList.Font = screenHandler.Fonts["myFontSmaller"];

            this.Components.Add(partList);

            partList.DrawItem += new DrawItemEventHandler(partList_DrawItem);
            partList.ItemSelected += new ItemSelectedEventHandler(partList_ItemSelected);

            // Wearing Equipment List
            wearingList = new ObjectListControl(Game, this);
            wearingList.Bounds = new Rectangle(1120, 140, 200, 300);
            wearingList.Font = screenHandler.Fonts["myFontSmaller"];
            wearingList.canUnselect = true;

            this.Components.Add(wearingList);

            wearingList.DrawItem += new DrawItemEventHandler(wearingList_DrawItem);
            wearingList.ItemSelected += new ItemSelectedEventHandler(wearingList_ItemSelected);

            // Holding Equipment List
            holdingList = new ObjectListControl(Game, this);
            holdingList.Bounds = new Rectangle(1330, 140, 200, 300);
            holdingList.Font = screenHandler.Fonts["myFontSmaller"];
            holdingList.canUnselect = true;

            this.Components.Add(holdingList);

            holdingList.DrawItem += new DrawItemEventHandler(holdingList_DrawItem);
            holdingList.ItemSelected += new ItemSelectedEventHandler(holdingList_ItemSelected);



            characterList.Select(0);

        }

        private void wearingList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Equipment thisEquipment = (Equipment)wearingList.SelectedItem;
            BodyPart thisPart = (BodyPart)partList.SelectedItem;
            Character thisCharacter = (Character)characterList.SelectedItem;

            if (!Party.ChangeEquipmentWorn(thisCharacter, thisPart, thisEquipment))
            {
                wearingList.Selected[e.Index] = false;
            }

        }

        private void wearingList_DrawItem(ObjectListControl sender, DrawItemEventArgs e)
        {
            Equipment thisEquipment = (Equipment)wearingList.Items[e.Index];

            if (wearingList.Selected[e.Index])
            {
                if (wearingList.HoverIndex == e.Index && wearingList.MouseInside)
                    sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds, Color.Red);
                else
                    sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds);
            }
            else if (wearingList.HoverIndex == e.Index && wearingList.MouseInside)
                sender.DrawLineItemHighlight(Color.Transparent, e.Bounds, Color.Red);

            e.SpriteBatch.DrawString(partList.Font, thisEquipment.Name, new Vector2(e.Bounds.Left + 20, e.Bounds.Top + (e.Bounds.Height - partList.Font.MeasureString(thisEquipment.Name).Y) / 2), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }

        private void holdingList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Equipment thisEquipment = (Equipment)holdingList.SelectedItem;
            BodyPart thisPart = (BodyPart)partList.SelectedItem;
            Character thisCharacter = (Character)characterList.SelectedItem;

            if (!Party.ChangeEquipmentHeld(thisCharacter, thisPart, thisEquipment))
            {
                holdingList.Selected[e.Index] = false;
            }

        }

        private void holdingList_DrawItem(ObjectListControl sender, DrawItemEventArgs e)
        {
            Equipment thisEquipment = (Equipment)holdingList.Items[e.Index];


            if (holdingList.Selected[e.Index])
            {
                if (holdingList.HoverIndex == e.Index && holdingList.MouseInside)
                    sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds, Color.Red);
                else
                    sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds);
            }
            else if (holdingList.HoverIndex == e.Index && holdingList.MouseInside)
                sender.DrawLineItemHighlight(Color.Transparent, e.Bounds, Color.Red);

            e.SpriteBatch.DrawString(partList.Font, thisEquipment.Name, new Vector2(e.Bounds.Left + 20, e.Bounds.Top + (e.Bounds.Height - partList.Font.MeasureString(thisEquipment.Name).Y) / 2), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }

        private void partList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Console.WriteLine("Selected " + e.Index);

            BodyPart thisPart = (BodyPart)partList.SelectedItem;

            if (thisPart.canHold)
            {
                holdingList.Clear();
                if (thisPart.Holding != null)
                    holdingList.AddItem(thisPart.Holding, true);
                foreach (var itemPair in Party.Items.Where(x => x.Value > 0 && x.Key is Equipment && ((Equipment)x.Key).HeldBy == thisPart.Type && ((Equipment)x.Key) != thisPart.Holding))
                {
                    Equipment equipment = (Equipment)itemPair.Key;
                    holdingList.AddItem(equipment, false);
                }
            }
            if (thisPart.canWear)
            {
                wearingList.Clear();
                if (thisPart.Wearing != null)
                    wearingList.AddItem(thisPart.Wearing, true);
                foreach (var itemPair in Party.Items.Where(x => x.Value > 0 && x.Key is Equipment && ((Equipment)x.Key).WornOn == thisPart.Type && ((Equipment)x.Key) != thisPart.Wearing))
                {
                    Equipment equipment = (Equipment)itemPair.Key;
                    wearingList.AddItem(equipment, false);
                }
            }

        }

         private void partList_DrawItem(ObjectListControl sender, DrawItemEventArgs e)
        {
            BodyPart thisPart = (BodyPart)partList.Items[e.Index];


            if (partList.Selected[e.Index])
            {
                sender.DrawLineItemHighlight(Color.AliceBlue, e.Bounds);
            }

            e.SpriteBatch.DrawString(partList.Font, thisPart.Name, new Vector2(e.Bounds.Left + 20, e.Bounds.Top + (e.Bounds.Height - partList.Font.MeasureString(thisPart.Name).Y) / 2), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }

        public override void Unload()
        {
            base.Unload();
            characterList.DrawItem -= new DrawItemEventHandler(characterList_DrawItem);
            characterList.ItemSelected -= new ItemSelectedEventHandler(characterList_ItemSelected);
            partList.DrawItem -= new DrawItemEventHandler(partList_DrawItem);
            partList.ItemSelected -= new ItemSelectedEventHandler(partList_ItemSelected);
        }

        private void characterList_ItemSelected(ObjectListControl sender, ItemSelctedEventArgs e)
        {
            Console.WriteLine("Selected " + e.Index);
            if (partList != null)
            {
                partList.Clear();
                foreach (BodyPart part in ((Character)characterList.SelectedItem).BodyParts)
                {
                    if (part.canHold || part.canWear)
                        partList.AddItem(part, false);
                    if (part.BodyParts == null)
                        continue;
                    foreach (BodyPart innerpart in part.BodyParts)
                    {
                        if (innerpart.canHold || innerpart.canWear)
                            partList.AddItem(innerpart, false);
                        if (innerpart.BodyParts == null)
                            continue;
                        foreach (BodyPart inmostpart in innerpart.BodyParts)
                        {
                            if (inmostpart.canHold || inmostpart.canWear)
                                partList.AddItem(inmostpart, false);
                        }
                    }
                }

            }
                partList.Select(0);
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
            Equipment thisWearing = (Equipment)wearingList.SelectedItem;
            Equipment thisHolding = (Equipment)holdingList.SelectedItem;

            if (thisCharacter == null)
                return;

            Rectangle StatusFrame = new Rectangle(470, 100, 1080, 650);

            DrawRect(Color.White, StatusFrame, Color.Black);

            Rectangle characterFrame = thisCharacter.GetAnimatedSpriteFrame(SpriteDirection.Front);

            SpriteBatch.Draw(screenHandler.CharacterSpriteTexture,
                new Rectangle(StatusFrame.Left + 10, StatusFrame.Top + 10, characterFrame.Width, characterFrame.Height),
                characterFrame, Color.White);

            SpriteBatch.DrawString(Font, thisCharacter.Name, new Vector2(StatusFrame.Left + 60, StatusFrame.Top + 8), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            SpriteBatch.DrawString(Font, "HP:" + thisCharacter.HP + " / " + thisCharacter.MaxHP, new Vector2(StatusFrame.Left + 200, StatusFrame.Top + 10), Color.Black,
                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            DrawRect(Color.DarkRed, new Rectangle(StatusFrame.Left + 200, StatusFrame.Top + 40, 200, 20), Color.Black);
            DrawRect(Color.Red, new Rectangle(StatusFrame.Left + 201, StatusFrame.Top + 40 + 1, (int)(198.0f * ((float)thisCharacter.HP / thisCharacter.MaxHP)), 19));

            Font = screenHandler.Fonts["myFontSmall"];

            if (wearingList.HoverIndex != -1 && wearingList.MouseInside || holdingList.HoverIndex != -1 && holdingList.MouseInside)
                DrawAlteredStats(thisCharacter, StatusFrame, SpriteBatch, gameTime);
            else
                DrawCharacterStats(thisCharacter, StatusFrame, SpriteBatch, gameTime);

            Font = screenHandler.Fonts["myFont"];

            // ListBox Labels

            SpriteBatch.DrawString(Font, "Body Parts", new Vector2(910, StatusFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            BodyPart thisPart = (BodyPart)partList.SelectedItem;
            if (thisPart != null)
            {
                wearingList.Visible = thisPart.canWear;
                if (thisPart.canWear)
                    SpriteBatch.DrawString(Font, "Wearing", new Vector2(1120, StatusFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

                holdingList.Visible = thisPart.canHold;
                if (thisPart.canHold)
                    SpriteBatch.DrawString(Font, "Holding", new Vector2(1330, StatusFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

            }



            // Description Frame

            Rectangle DescriptionFrame = new Rectangle(470, 770, 1080, 80);

            DrawRect(Color.White, DescriptionFrame, Color.Black);

            if (partList.MouseInside)
            {
                if (partList.HoverIndex == -1 && thisPart != null)
                    SpriteBatch.DrawString(Font, thisPart.Name, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                else
                    SpriteBatch.DrawString(Font, ((BodyPart)partList.Items[partList.HoverIndex]).Name, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            }
            else if (wearingList.MouseInside && wearingList.Visible)
            {
                if (wearingList.HoverIndex == -1 && thisWearing != null)
                    SpriteBatch.DrawString(Font, thisWearing.Name, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                else if (wearingList.HoverIndex != -1)
                    SpriteBatch.DrawString(Font, ((Equipment)wearingList.Items[wearingList.HoverIndex]).Name, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            }
            else if (holdingList.MouseInside && holdingList.Visible)
            {
                if (holdingList.HoverIndex == -1 && thisHolding != null)
                    SpriteBatch.DrawString(Font, thisHolding.Name, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                else if (holdingList.HoverIndex != -1)
                    SpriteBatch.DrawString(Font, ((Equipment)holdingList.Items[holdingList.HoverIndex]).Name, new Vector2(DescriptionFrame.Left + 10, DescriptionFrame.Top), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            }

            base.Draw(SpriteBatch, gameTime);

        }

        private void DrawAlteredStats(Character thisCharacter, Rectangle StatusFrame, SpriteBatch SpriteBatch, GameTime gameTime)
        {
            Dictionary<string, float> Stats = thisCharacter.GetStatsList();
            Dictionary<string, float> SecondaryStats = thisCharacter.GetSecondaryStatsList();

            Dictionary<string, float> AlteredStats = Stats;
            Dictionary<string, float> AlteredSecondaryStats = SecondaryStats;

            if (wearingList.MouseInside && wearingList.Visible)
            {
                if (wearingList.SelectedIndex == wearingList.HoverIndex) // If temp removing
                {
                    AlteredStats = thisCharacter.GetAlteredStatsListWearing((BodyPart)partList.SelectedItem, null);
                    AlteredSecondaryStats = thisCharacter.GetAlteredSecondaryStatsListWearing((BodyPart)partList.SelectedItem, null);
                }
                else
                {
                    AlteredStats = thisCharacter.GetAlteredStatsListWearing((BodyPart)partList.SelectedItem, (Equipment)(wearingList.Items[wearingList.HoverIndex]));
                    AlteredSecondaryStats = thisCharacter.GetAlteredSecondaryStatsListWearing((BodyPart)partList.SelectedItem, (Equipment)(wearingList.Items[wearingList.HoverIndex]));
                }
            }
            else if (holdingList.MouseInside && holdingList.Visible)
            {
                if (holdingList.SelectedIndex == holdingList.HoverIndex) // If temp removing
                {
                    AlteredStats = thisCharacter.GetAlteredStatsListHolding((BodyPart)partList.SelectedItem, null);
                    AlteredSecondaryStats = thisCharacter.GetAlteredSecondaryStatsListHolding((BodyPart)partList.SelectedItem, null);
                }
                else
                {
                    AlteredStats = thisCharacter.GetAlteredStatsListHolding((BodyPart)partList.SelectedItem, (Equipment)(holdingList.Items[holdingList.HoverIndex]));
                    AlteredSecondaryStats = thisCharacter.GetAlteredSecondaryStatsListHolding((BodyPart)partList.SelectedItem, (Equipment)(holdingList.Items[holdingList.HoverIndex]));
                }
            }


            int i = 0;
            int y = StatusFrame.Top + 50;
            foreach (string stat in Stats.Keys)
            {
                Color drawColor = Color.Black;
                if (AlteredStats[stat] > Stats[stat])
                    drawColor = Color.Blue;
                else if (AlteredStats[stat] < Stats[stat])
                    drawColor = Color.Red;

                SpriteBatch.DrawString(Font, stat.PadRight(13, '.') + AlteredStats[stat].ToString().PadLeft(3, '.'), new Vector2(StatusFrame.Left + 10, y), drawColor,
                    0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

                i++;
                y += (int)Font.MeasureString(stat).Y;
            }

            y += 10;
            i = 0;
            foreach (string stat in SecondaryStats.Keys)
            {
                Color drawColor = Color.Black;
                if (AlteredSecondaryStats[stat] > SecondaryStats[stat])
                    drawColor = Color.Blue;
                else if (AlteredSecondaryStats[stat] < SecondaryStats[stat])
                    drawColor = Color.Red;

                if (SecondaryStats[stat].ToString().Contains('.'))
                    SpriteBatch.DrawString(Font, stat.PadRight(23, '.') + Math.Round(AlteredSecondaryStats[stat], 2).ToString("0.00").PadLeft(6, '.'), new Vector2(StatusFrame.Left + 10, y), drawColor,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                else
                    SpriteBatch.DrawString(Font, stat.PadRight(23, '.') + Math.Round(AlteredSecondaryStats[stat], 2).ToString().PadLeft(3, '.'), new Vector2(StatusFrame.Left + 10, y), drawColor,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);


                i++;
                y += (int)Font.MeasureString(stat).Y;
            }
        }

        private void DrawCharacterStats(Character thisCharacter, Rectangle StatusFrame, SpriteBatch SpriteBatch, GameTime gameTime)
        {
            Dictionary<string, float> Stats = thisCharacter.GetStatsList();

            int i = 0;
            int y = StatusFrame.Top + 50;
            foreach (string stat in Stats.Keys)
            {
                SpriteBatch.DrawString(Font, stat.PadRight(13, '.') + Stats[stat].ToString().PadLeft(3, '.'), new Vector2(StatusFrame.Left + 10, y), Color.Black,
                    0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                i++;
                y += (int)Font.MeasureString(stat).Y;
            }

            y += 10;
            Dictionary<string, float> SecondaryStats = thisCharacter.GetSecondaryStatsList();

            i = 0;
            foreach (string stat in SecondaryStats.Keys)
            {
                if (SecondaryStats[stat].ToString().Contains('.'))
                    SpriteBatch.DrawString(Font, stat.PadRight(23, '.') + Math.Round(SecondaryStats[stat], 2).ToString("0.00").PadLeft(6, '.'), new Vector2(StatusFrame.Left + 10, y), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                else
                    SpriteBatch.DrawString(Font, stat.PadRight(23, '.') + Math.Round(SecondaryStats[stat], 2).ToString().PadLeft(3, '.'), new Vector2(StatusFrame.Left + 10, y), Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);


                i++;
                y += (int)Font.MeasureString(stat).Y;
            }

        }
    }
}
