using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;
using Red_Sky.ControlClasses;
using Red_Sky.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    class MenuScreen : GameScreen
    {
        public Party Party { get; set; }

        public MenuScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
            : base(width, height, game, manager)
        {

        }

        internal override void LoadContent()
        {
            base.LoadContent();

            Components.Add(Game.World);
            Party = Game.World.Player;

            createNavBar();
        }

        public override void MouseButtonPressed(Nuclex.Input.MouseButtons buttons)
        {
            base.MouseButtonPressed(buttons);


        }

        public override void MouseButtonReleased(Nuclex.Input.MouseButtons buttons)
        {
            base.MouseButtonReleased(buttons);

            if (buttons == Nuclex.Input.MouseButtons.Left)
            {
                foreach (var component in Components)
                {
                    if (component is ObjectListControl)
                    {
                        ObjectListControl list = ((ObjectListControl)component);
                        if (list.Bounds.Contains(new Point((int)MouseLoc.X, (int)MouseLoc.Y)))
                        {
                            list.MouseClicked(MouseLoc);
                            break;
                        }

                    }
                }

                buttons = 0;
            }
        }

        public override void MouseMoved(float x, float y)
        {
            base.MouseMoved(x, y);

            foreach (var component in Components)
            {
                if (component is ObjectListControl)
                {
                    ObjectListControl list = ((ObjectListControl)component);
                    if (list.Bounds.Contains(new Point((int)MouseLoc.X, (int)MouseLoc.Y)))
                    {
                        //If just entered the list
                        if (!list.Bounds.Contains(new Point((int)OldMouseLoc.X, (int)OldMouseLoc.Y)))
                            list.MouseEntered(MouseLoc);

                        list.MouseMoved(MouseLoc);
                    }
                    else if (list.Bounds.Contains(new Point((int)OldMouseLoc.X, (int)OldMouseLoc.Y)))
                    {
                        list.MouseLeft(MouseLoc);
                    }
                }
            }
        }


        internal override void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {

            foreach (var component in Components)
            {
                if (component is ObjectListControl)
                {
                    var com = ((ObjectListControl)component);

                    com.Draw(SpriteBatch, gameTime);
                    
                }
            }

        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }

        private void createNavBar()
        {
            const int ButtonWidth = 100;
            const int ButtonHeight = 32;
            const int ButtonSpacing = 10;
            const int buttons = 6;

            float leftEdge = (this.Desktop.GetAbsoluteBounds().Width - buttons * ButtonWidth - (buttons - 1) * ButtonSpacing) / 2 / this.Desktop.GetAbsoluteBounds().Width;

            ButtonControl partyButton = new ButtonControl();
            partyButton.Text = "Party";
            partyButton.Bounds = new UniRectangle(
              new UniScalar(leftEdge, 0.0f), new UniScalar(0.0f, 20.0f), 100, 32
            );
            partyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.ChangeScreen<MenuPartyScreen>();
            };


            this.Desktop.Children.Add(partyButton);

            ButtonControl itemButton = new ButtonControl();
            itemButton.Text = "Item";
            itemButton.Bounds = new UniRectangle(
              new UniScalar(leftEdge, (ButtonWidth + ButtonSpacing)), new UniScalar(0.0f, 20.0f), 100, 32
            );
            itemButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.ChangeScreen<MenuItemScreen>();
            };
            this.Desktop.Children.Add(itemButton);


            ButtonControl equipButton = new ButtonControl();
            equipButton.Text = "Equip";
            equipButton.Bounds = new UniRectangle(
              new UniScalar(leftEdge, (ButtonWidth + ButtonSpacing) * 2), new UniScalar(0.0f, 20.0f), 100, 32
            );
            equipButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.ChangeScreen<MenuEquipScreen>();
            };
            this.Desktop.Children.Add(equipButton);


            ButtonControl statusButton = new ButtonControl();
            statusButton.Text = "Status";
            statusButton.Bounds = new UniRectangle(
              new UniScalar(leftEdge, (ButtonWidth + ButtonSpacing) * 3), new UniScalar(0.0f, 20.0f), 100, 32
            );
            statusButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.ChangeScreen<MenuStatusScreen>();
            };
            this.Desktop.Children.Add(statusButton);

            ButtonControl skillButton = new ButtonControl();
            skillButton.Text = "Skill";
            skillButton.Bounds = new UniRectangle(
              new UniScalar(leftEdge, (ButtonWidth + ButtonSpacing) * 4), new UniScalar(0.0f, 20.0f), 100, 32
            );
            skillButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.ChangeScreen<MenuSkillScreen>();
            };
            this.Desktop.Children.Add(skillButton);

            ButtonControl backButton = new ButtonControl();
            backButton.Text = "Back";
            backButton.Bounds = new UniRectangle(
              new UniScalar(leftEdge, (ButtonWidth + ButtonSpacing) * 5), new UniScalar(0.0f, 20.0f), 80, 32
            );
            backButton.Pressed += delegate(object sender, EventArgs arguments) { screenHandler.PopScreen(); };
            this.Desktop.Children.Add(backButton);
        }
    }
}