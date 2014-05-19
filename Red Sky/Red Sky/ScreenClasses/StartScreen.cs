using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    class StartScreen : GameScreen
    {

        public StartScreen(int width, int height, RedSkyGame game, ScreenHandler manager)
            :base(width, height, game, manager)
        {
            
        }

        internal override void Initialize()
        {
            base.Initialize();

            createDesktopControls();
        }

        private void createDesktopControls()
        {

            
            ButtonControl newGameButton = new ButtonControl();
            newGameButton.Text = "New Game";
            newGameButton.Bounds = new UniRectangle(
              new UniScalar(0.9f, -300.0f), new UniScalar(0.9f, -32.0f), 100, 32
            );
            newGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                screenHandler.AddScreen<NewGameScreen>();
            };

            
            this.Desktop.Children.Add(newGameButton);



            
            ButtonControl loadGameButton = new ButtonControl();
            loadGameButton.Text = "Load Game";
            loadGameButton.Bounds = new UniRectangle(
              new UniScalar(0.9f, -190.0f), new UniScalar(0.9f, -32.0f), 100, 32
            );
            loadGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                //screenHandler.AddScreen<loadGameScreen>();

            };
            this.Desktop.Children.Add(loadGameButton);

            // Button through which the user can quit the application
            ButtonControl quitButton = new ButtonControl();
            quitButton.Text = "Quit";
            quitButton.Bounds = new UniRectangle(
              new UniScalar(0.9f, -80.0f), new UniScalar(0.9f, -32.0f), 80, 32
            );
            quitButton.Pressed += delegate(object sender, EventArgs arguments) {  Game.Exit(); };
            this.Desktop.Children.Add(quitButton);

        }
    }
}
