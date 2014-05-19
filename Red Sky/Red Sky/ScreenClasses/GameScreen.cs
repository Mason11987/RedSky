using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nuclex.Input.Devices;
using Nuclex.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Red_Sky.ScreenClasses
{
    public class GameScreen : Screen
    {
        public ScreenHandler screenHandler;
        protected Vector2 MouseLoc;
        protected Vector2 OldMouseLoc;

        List<GameComponent> childComponents;
        public SpriteFont Font;

        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

        protected RedSkyGame Game;

        public GameScreen(int width, int height, RedSkyGame game, ScreenHandler handler)
            : base(width, height)
        {
            screenHandler = handler;
            Game = game;
            childComponents = new List<GameComponent>();
        }



        internal virtual void Initialize()
        {
            this.Desktop.Bounds = new UniRectangle(
                  new UniScalar(0.0f, 0.0f), new UniScalar(0.0f, 0.0f), // x and y = 10%
                  new UniScalar(1.0f, 0.0f), new UniScalar(1.0f, 0.0f) // width and height = 80%
                );

            // Whenever a key is pressed on the keyboard, call the charEntered() method
            // (see below) so the game can do something with the character.
            IKeyboard keyboard = Game.input.GetKeyboard();
            keyboard.KeyPressed += new KeyDelegate(KeyPressed);
            keyboard.KeyReleased += new KeyDelegate(KeyReleased);
            IMouse mouse = Game.input.GetMouse();
            mouse.MouseMoved += new MouseMoveDelegate(MouseMoved);
            mouse.MouseButtonPressed += new MouseButtonDelegate(MouseButtonPressed);
            mouse.MouseButtonReleased += new MouseButtonDelegate(MouseButtonReleased);
            mouse.MouseWheelRotated += new MouseWheelDelegate(MouseWheelRotated);

            LoadContent();
        }

        public virtual void Unload()
        {
            IKeyboard keyboard = Game.input.GetKeyboard();
            keyboard.KeyPressed -= new KeyDelegate(KeyPressed);
            keyboard.KeyReleased -= new KeyDelegate(KeyReleased);
            IMouse mouse = Game.input.GetMouse();
            mouse.MouseMoved -= new MouseMoveDelegate(MouseMoved);
            mouse.MouseButtonPressed -= new MouseButtonDelegate(MouseButtonPressed);
            mouse.MouseButtonReleased -= new MouseButtonDelegate(MouseButtonReleased);
            mouse.MouseWheelRotated -= new MouseWheelDelegate(MouseWheelRotated);
            
        }

        public virtual void KeyPressed(Keys key)
        {

        }

        public virtual void KeyReleased(Keys key)
        {
            
        }

        public virtual void MouseWheelRotated(float ticks)
        {
            
        }

        public virtual void MouseButtonReleased(Nuclex.Input.MouseButtons buttons)
        {
            
        }

        public virtual void MouseButtonPressed(Nuclex.Input.MouseButtons buttons)
        {
               
        }

        public virtual void MouseMoved(float x, float y)
        {
            OldMouseLoc = new Vector2(MouseLoc.X, MouseLoc.Y);
            MouseLoc = new Vector2(x, y);
        }


        internal virtual void LoadContent()
        {

            Font = screenHandler.Fonts["myFont"];
        }

        internal virtual void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {
            foreach (var Component in Components)
            {
                if (Component is DrawableGameComponent && (Component as DrawableGameComponent).Visible)
                    (Component as DrawableGameComponent).Draw(gameTime);
            }
        }

        public void DrawRect(Color fillcolor, Rectangle bounds)
        {
            Game.basicEffect.CurrentTechnique.Passes[0].Apply();
            VertexPositionColor[] vertices;
            vertices = new VertexPositionColor[6];

            vertices[0] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Top, 0), fillcolor);
            vertices[1] = new VertexPositionColor(new Vector3(bounds.Right, bounds.Top, 0), fillcolor);
            vertices[2] = new VertexPositionColor(new Vector3(bounds.Right, bounds.Bottom, 0), fillcolor);
            vertices[3] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Bottom, 0), fillcolor);
            vertices[4] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Top, 0), fillcolor);

            Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, 3);
        }

        public void DrawRect(Color fillcolor, Rectangle bounds, Color borderColor)
        {
            Game.basicEffect.CurrentTechnique.Passes[0].Apply();
            VertexPositionColor[] vertices;
            vertices = new VertexPositionColor[6];

            vertices[0] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Top, 0), fillcolor);
            vertices[1] = new VertexPositionColor(new Vector3(bounds.Right, bounds.Top, 0), fillcolor);
            vertices[2] = new VertexPositionColor(new Vector3(bounds.Right, bounds.Bottom, 0), fillcolor);
            vertices[3] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Bottom, 0), fillcolor);
            vertices[4] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Top, 0), fillcolor);

            Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, 3);

            vertices[0].Color = borderColor;
            vertices[1].Color = borderColor;
            vertices[2].Color = borderColor;
            vertices[3].Color = borderColor;
            vertices[4].Color = borderColor;

            Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices, 0, 4);
        }

        internal virtual void Update(GameTime gameTime)
        {
            foreach (var Component in Components)
            {
                if (Component.Enabled)
                    Component.Update(gameTime);
            }

        }

        internal bool IsMouseOver(Rectangle rect)
        {

            return rect.Contains(new Point((int)MouseLoc.X, (int)MouseLoc.Y));
        }
    }
}
