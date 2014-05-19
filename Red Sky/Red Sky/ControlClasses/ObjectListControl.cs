using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Red_Sky.ScreenClasses;
using System.Diagnostics;

namespace Red_Sky.ControlClasses
{

    public delegate void DrawItemEventHandler(ObjectListControl sender, DrawItemEventArgs e);
    public delegate void ItemSelectedEventHandler(ObjectListControl sender, ItemSelctedEventArgs e);


    public class DrawItemEventArgs : EventArgs
    {
        public DrawItemEventArgs(SpriteBatch spritebatch, Rectangle rect, int index)
        {
            Index = index;
            Bounds = rect;
            SpriteBatch = spritebatch;
        }

        public Rectangle Bounds { get; private set; }

        public int Index { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }
    }


    public class ItemSelctedEventArgs : EventArgs
    {
        public ItemSelctedEventArgs(int index)
        {
            Index = index;
        }

        public int Index { get; private set; }
    }

    public class ObjectListControl : DrawableGameComponent
    {
        VertexPositionColor[] vertices;
        public event DrawItemEventHandler DrawItem;
        public event ItemSelectedEventHandler ItemSelected;
        public bool multiSelect;
        public bool canUnselect;
        internal GameScreen ParentScreen;

        private SpriteFont font;
        public SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                if (value != font)
                {
                    font = value;
                    ItemHeight = (int)font.MeasureString("SampleString").Y;
                }
            }
        }
        public int ItemHeight { get; set; }
        private int TopItem;
        private int NavWidth;
        private int ItemsViewable;

        public List<object> Items { get; set; }
        public List<bool> Selected { get; set; }
        public Rectangle Bounds { get; set; }
        public int HoverIndex { get; set; }
        
        public new RedSkyGame Game;

        public bool MouseInside
        {
            get { return ParentScreen.IsMouseOver(Bounds); }
        }

        public bool canScrollUp
        {
            get
            {
                return TopItem > 0;
            }
        }

        public bool canScrollDown
        {
            get
            {
                return TopItem + ItemsViewable < Items.Count;
            }
        }

        public bool DrawScrollBar
        {
            get
            {
                return canScrollDown || canScrollUp;
            }
        }

        private Rectangle ScrollBarTrackArea
        {
            get
            {
                return new Rectangle(Bounds.Right - NavWidth + 1, Bounds.Top + NavWidth + 1, NavWidth - 1, Bounds.Height - (NavWidth * 2) - 1);
            }
        }

        private Rectangle ScrollThumbArea
        {
            get
            {
                Rectangle SBTA = ScrollBarTrackArea;
                return new Rectangle(SBTA.Left + 1, (int)(SBTA.Top + 1 + (float)(SBTA.Height - 2) / Items.Count * TopItem), SBTA.Width - 3, (int)Math.Floor((SBTA.Height - 2) / ((float)Items.Count / ItemsViewable)));
            }
        }


        internal ObjectListControl(RedSkyGame game, GameScreen parentScreen)
            : base(game)
        {
            Game = game;
            ParentScreen = parentScreen;
            Items = new List<object>();
            Selected = new List<bool>();
            TopItem = 0;
            NavWidth = 26;
            ItemsViewable = 0;
            HoverIndex = -1;
            canUnselect = false;
            multiSelect = false;
            Visible = true;
            Enabled = true;
            LoadContent();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Font = Game.screenHandler.Fonts["myFont"];
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                base.Draw(gameTime);

                DrawBorder();

                ItemsViewable = (int)(Bounds.Height / ItemHeight);

                for (int i = 0; i < Items.Count; i++)
                {
                    if (i * ItemHeight + Bounds.Top + ItemHeight <= Bounds.Bottom && Items.Count > i + TopItem)
                        DrawItem(this, new DrawItemEventArgs(spriteBatch, new Rectangle(Bounds.Left, i * ItemHeight + Bounds.Top, Bounds.Width - (DrawScrollBar ? NavWidth : 0), ItemHeight), i + TopItem));

                    //DrawListObject(Items[i], new Vector2(0,i * ItemHeight));
                }

                DrawNav(spriteBatch);
            }
        }

        private void DrawNav(SpriteBatch spriteBatch)
        {

            if (DrawScrollBar)
            {
                Game.basicEffect.CurrentTechnique.Passes[0].Apply();
                vertices = new VertexPositionColor[6];

                vertices[0] = new VertexPositionColor(new Vector3(Bounds.Right - NavWidth, Bounds.Top, 0), Color.Black);
                vertices[1] = new VertexPositionColor(new Vector3(Bounds.Right - NavWidth, Bounds.Bottom, 0), Color.Black);

                Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, 1);

                spriteBatch.DrawString(Game.screenHandler.Fonts["myFont"], "^", new Vector2(Bounds.Right - 20, Bounds.Top), canScrollUp ? Color.Black : Color.LightGray,
                                0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

                vertices[0] = new VertexPositionColor(new Vector3(Bounds.Right, Bounds.Top + NavWidth, 0), Color.Black);
                vertices[1] = new VertexPositionColor(new Vector3(Bounds.Right - NavWidth, Bounds.Top + NavWidth, 0), Color.Black);

                Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, 1);


                spriteBatch.DrawString(Game.screenHandler.Fonts["myFont"], "^", new Vector2(Bounds.Right - 5, Bounds.Bottom + 1), canScrollDown ? Color.Black : Color.LightGray,
                            MathHelper.Pi, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

                vertices[0] = new VertexPositionColor(new Vector3(Bounds.Right, Bounds.Bottom - NavWidth, 0), Color.Black);
                vertices[1] = new VertexPositionColor(new Vector3(Bounds.Right - NavWidth, Bounds.Bottom - NavWidth, 0), Color.Black);

                Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, 1);
                

                DrawRect(Color.GhostWhite, ScrollBarTrackArea);

                DrawRect(Color.White, ScrollThumbArea, Color.Black);
            }



        }

        private void DrawBorder()
        {
            Game.basicEffect.CurrentTechnique.Passes[0].Apply();

            DrawRect(Color.White, Bounds, Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }


        internal void AddItem(object obj, bool selected)
        {
            Items.Add(obj);
            Selected.Add(selected);
        }

        internal void Clear()
        {
            Items.Clear();
            Selected.Clear();
        }

        public void Select(int index)
        {
            if (Selected.Count <= index)
                return;
            if (!canUnselect && Selected[index]) 
            {
                // If can't unselect, and clicked selected item, do nothing
                
            }
            else 
            {
                Selected[index] = !Selected[index];
                if (!multiSelect)
                {
                    for (int i = 0; i < Selected.Count; i++)
                    {
                        if (i != index)
                            Selected[i] = false;
                    }
                }
                if (ItemSelected != null)
                    ItemSelected(this, new ItemSelctedEventArgs(index));
            }



        }

        internal void MouseClicked(Vector2 MouseLoc)
        {
            if (!Visible && !Enabled)
                return;

            Vector2 OffsetLoc = new Vector2(MouseLoc.X - Bounds.Left, MouseLoc.Y - Bounds.Top);

            int selectedIndex = GetIndexAt(OffsetLoc);


            if (selectedIndex < Items.Count && selectedIndex >= 0)
                Select(selectedIndex);
            else if (DrawScrollBar && OffsetLoc.X > Bounds.Width - NavWidth)
            {
                if (OffsetLoc.Y < NavWidth && canScrollUp) // On top arrow
                    TopItem--;
                else if (OffsetLoc.Y > Bounds.Height - NavWidth && canScrollDown)
                    TopItem++;
                else if (MouseLoc.Y < ScrollThumbArea.Top && canScrollUp)
                    TopItem -= ItemsViewable;
                else if (MouseLoc.Y > ScrollThumbArea.Bottom && canScrollDown)
                    TopItem += ItemsViewable;

                TopItem = (int)MathHelper.Clamp(TopItem, 0, Items.Count - ItemsViewable);
            }
        }

        private int GetIndexAt(Vector2 OffsetLoc)
        {
            if (DrawScrollBar && OffsetLoc.X > Bounds.Width - NavWidth)
                return -1;

            int selectedPos = (int)OffsetLoc.Y / ItemHeight;
            if (selectedPos * ItemHeight + Bounds.Top + ItemHeight <= Bounds.Bottom)
            {
                int selectedIndex = selectedPos + TopItem;
                if (selectedIndex < Items.Count)
                    return selectedIndex;
            }
            return -1;
        }

        internal void MouseMoved(Vector2 MouseLoc)
        {
            if (!Visible && !Enabled)
                return;

            Vector2 OffsetLoc = new Vector2(MouseLoc.X - Bounds.Left, MouseLoc.Y - Bounds.Top);

            int selectedIndex = GetIndexAt(OffsetLoc);

            HoverIndex = selectedIndex;

            //if (selectedIndex < Items.Count && selectedIndex >= 0)

        }

        internal void MouseEntered(Vector2 MouseLoc)
        {
            //throw new NotImplementedException();
        }

        internal void MouseLeft(Vector2 MouseLoc)
        {
            //throw new NotImplementedException();
            HoverIndex = -1;
        }

        internal void DrawLineItemHighlight(Color color, Rectangle bounds)
        {
            DrawRect(color, new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 2));
        }

        internal void DrawLineItemHighlight(Color fillcolor, Rectangle bounds, Color borderColor)
        {
            DrawRect(fillcolor, new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 2), borderColor);

        }

        internal void DrawRect(Color color, Rectangle bounds)
        {
            VertexPositionColor[] vertices;
            vertices = new VertexPositionColor[6];

            vertices[0] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Top, 0), color);
            vertices[1] = new VertexPositionColor(new Vector3(bounds.Right, bounds.Top, 0), color);
            vertices[2] = new VertexPositionColor(new Vector3(bounds.Right, bounds.Bottom, 0), color);
            vertices[3] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Bottom, 0), color);
            vertices[4] = new VertexPositionColor(new Vector3(bounds.Left, bounds.Top, 0), color);

            Game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, 3);
        }

        public void DrawRect(Color fillcolor, Rectangle bounds, Color borderColor)
        {
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

        public int SelectedIndex
        {
            get
            {
                if (!multiSelect)
                {
                    for (int i = 0; i < Selected.Count; i++)
                    {

                        if (Selected[i])
                            return i;
                    }
                }
                return -1;
            }
        }


        public object SelectedItem
        {
            get
            {
                if (!multiSelect)
                {
                    for (int i = 0; i < Selected.Count; i++)
                    {

                        if (Selected[i])
                            return Items[i];
                    }
                }
                return null;
            }
        }

    }
}
