using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls.Desktop;
using Red_Sky.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    class NewGameScreen : GameScreen
    {

        public NewGameScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
            : base(width, height, game, manager)
        {

        }

        internal override void Initialize()
        {
            base.Initialize();

            createDesktopControls();
        }

        private void createDesktopControls()
        {

            // Button to open another "New Game" dialog
            ButtonControl newGameButton = new ButtonControl();
            newGameButton.Text = "Start Game";
            newGameButton.Bounds = new UniRectangle(
              new UniScalar(0.9f, -300.0f), new UniScalar(0.9f, -32.0f), 100, 32
            );
            newGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                Game.World = World.StartNew(Game);
                screenHandler.AddScreen<MapScreen>();
            };


            this.Desktop.Children.Add(newGameButton);



            // Button through which the user can quit the application
            ButtonControl backButton = new ButtonControl();
            backButton.Text = "Back";
            backButton.Bounds = new UniRectangle(
              new UniScalar(0.9f, -80.0f), new UniScalar(0.9f, -32.0f), 80, 32
            );
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.AddScreen<StartScreen>();
            };
            this.Desktop.Children.Add(backButton);

        }
    }
}
