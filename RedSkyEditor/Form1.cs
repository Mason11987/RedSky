using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RedSkyData;

namespace RedSkyContent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Dictionary<int, MapData> Maps = new Dictionary<int, MapData>();
        Point HoverTile;
        int SelectedSheet;
        Point SelectedTile;

        Dictionary<int, Image> TileSheets = new Dictionary<int, Image>();

        //Home Laptop
        //const string GameFolder = @"D:\Documents\SkyDrive\Documents\Code\DotNet\Red Sky\";
        //const string TileSheetFolder = @"D:\Documents\SkyDrive\Documents\Code\DotNet\Red Sky\Red Sky\Red SkyContent\Map Tiles\";

        //Work Laptop
        const string GameFolder = @"C:\OneDrive\Documents\Code\DotNet\RedSky\";
        const string TileSheetFolder = @"C:\OneDrive\Documents\Code\DotNet\RedSky\Red Sky\Red SkyContent\Map Tiles\";

        private void Form1_Load(object sender, EventArgs e)
        {
            DataManager.Connect(GameFolder + @"Red Sky\Red Sky\RedSkyData.s3db");

            int maxW = 0;
            int maxH = 0;
            foreach (string file in Directory.GetFiles(TileSheetFolder))
            {
                int Sheet = int.Parse(Path.GetFileNameWithoutExtension(file));
                TileSheets.Add(Sheet, Image.FromFile(file));
                if (TileSheets[Sheet].Width > maxW)
                    maxW = TileSheets[Sheet].Width;
                if (TileSheets[Sheet].Height > maxH)
                    maxH = TileSheets[Sheet].Height;

                lstTileSets.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            lstTileSets.SelectedIndex = 0;


            LoadAllMapData();
            PopulateAllMapData();

            //Random rnd = new Random();

            //Maps[1].Width = 64;
            //Maps[1].Height = 64;

            //int layer = Maps[1].Layers[0].ID;
            //DataManager.BeginTransaction();

            //for (int x = 0; x < 64; x++)
            //{
            //    for (int y = 0; y < 64; y++)
            //    {
            //        TileData t = new TileData(x, y, layer, 1, rnd.Next(1, 10), rnd.Next(1, 10));
            //        DataManager.Update(t);
            //    }
            //}
            //DataManager.Update(Maps[1]);
            //DataManager.Commit();
            
        }

        private void LoadAllMapData()
        {
            var MapsList = DataManager.LoadMaps();
            Maps.Clear();
            foreach (MapData map in MapsList)
            {
                Maps.Add(map.ID, map);
            }
        }

        private void PopulateAllMapData()
        {
            lstMaps.Items.Clear();
            foreach (MapData map in Maps.Values)
            {
                lstMaps.Items.Add(map);
            }
            lstMaps.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void lstMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMaps.SelectedItem == null)
                return;
            LoadSelectedMap();
        }

        private void LoadSelectedMap()
        {
            lstLayers.Items.Clear();
            foreach (LayerData layer in ((MapData)lstMaps.SelectedItem).Layers)
            {
                lstLayers.Items.Add(layer);
            }
            lstLayers.SelectedIndex = 0;

            txtMapWidth.Text = ((MapData)lstMaps.SelectedItem).Width.ToString();
            txtMapHeight.Text = ((MapData)lstMaps.SelectedItem).Height.ToString();
        }



        private void lstLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayerData layer = (LayerData)lstLayers.SelectedItem;

            int vShowing = picLayer.Height / 32;
            int hShowing = picLayer.Width / 32;
            if (vShowing < Maps[layer.Map].Height)
            {
                vscrMap.Visible = true;
                vscrMap.Maximum = Maps[layer.Map].Height - vShowing;
            }
            else
                vscrMap.Visible = false;
            if (hShowing < Maps[layer.Map].Width)
            {
                hscrMap.Visible = true;
                hscrMap.Maximum = Maps[layer.Map].Width - hShowing;
            }
            else
                hscrMap.Visible = false;

            if (chkDrawAll.Checked)
                DrawMap();
            else
                DrawMap(layer);
        }

        private void DrawMap(LayerData curLayer)
        {

            Image img = new Bitmap(picLayer.Width, picLayer.Height);
            Graphics g = Graphics.FromImage(img);
            Rectangle ViewPort = new Rectangle(hscrMap.Value * 32, vscrMap.Value * 32, picLayer.Width, picLayer.Height);
            DrawLayer(curLayer, g, ViewPort);
            picLayer.Image = img;
        }


        private void DrawMap(LayerData curLayer, Point HoverTile)
        {
            Image img = new Bitmap(picLayer.Width, picLayer.Height);
            Graphics g = Graphics.FromImage(img);
            Rectangle ViewPort = new Rectangle(hscrMap.Value * 32, vscrMap.Value * 32, picLayer.Width, picLayer.Height);
            DrawLayer(curLayer, g, ViewPort, HoverTile);
            picLayer.Image = img;

        }

        private void DrawLayer(LayerData curLayer, Graphics g, Rectangle ViewPort)
        {
            foreach (var TileData in curLayer.Tiles)
            {
                if (!(TileData.Sheet == 1 && TileData.SheetX == 1 && TileData.SheetY == 1) && 
                    (TileData.X < Maps[curLayer.Map].Width && TileData.Y < Maps[curLayer.Map].Height))
                    g.DrawImage(TileSheets[TileData.Sheet],
                        new Rectangle(TileData.X * 32 - ViewPort.Left, TileData.Y * 32 - ViewPort.Top, 32, 32),
                        new Rectangle(TileData.SheetX * 32, TileData.SheetY * 32, 32, 32),
                        GraphicsUnit.Pixel);
            }
        }


        private void DrawLayer(LayerData curLayer, Graphics g, Rectangle ViewPort, Point HoverTile)
        {
            foreach (var TileData in curLayer.Tiles)
            {
                if (TileData.X == HoverTile.X && TileData.Y == HoverTile.Y && 
                    !(TileData.Sheet == 1 && TileData.SheetX == 1 && TileData.SheetY == 1))
                {

                    g.DrawImage(TileSheets[TileData.Sheet],
                        new Rectangle(TileData.X * 32 - ViewPort.Left, TileData.Y * 32 - ViewPort.Top, 32, 32),
                        new Rectangle(TileData.SheetX * 32, TileData.SheetY * 32, 32, 32),
                        GraphicsUnit.Pixel);
                    break;
                }
            }
        }

        private void DrawMap()
        {
            Image img = new Bitmap(picLayer.Width, picLayer.Height);
            Graphics g = Graphics.FromImage(img);
            Rectangle ViewPort = new Rectangle(hscrMap.Value * 32, vscrMap.Value * 32, picLayer.Width, picLayer.Height);
            if (chkDrawAll.Checked)
            {
                foreach (LayerData layer in ((MapData)lstMaps.SelectedItem).Layers)
                {
                    DrawLayer(layer, g, ViewPort);
                }
            }
            else
                DrawLayer((LayerData)lstLayers.SelectedItem, g, ViewPort);

            picLayer.Image = img;
        }



        private void DrawMap(Point HoverTile)
        {
            Image img = picLayer.Image;

            Graphics g = Graphics.FromImage(picLayer.Image);
            Rectangle ViewPort = new Rectangle(hscrMap.Value * 32, vscrMap.Value * 32, picLayer.Width, picLayer.Height);
            if (chkDrawAll.Checked)
            {
                g.Clip = new Region(new Rectangle(HoverTile.X * 32 - ViewPort.Left, HoverTile.Y * 32 - ViewPort.Top, 32, 32));
                g.Clear(Color.Transparent);
                foreach (LayerData layer in ((MapData)lstMaps.SelectedItem).Layers)
                {
                    DrawLayer(layer, g, ViewPort, HoverTile);
                }
            }
            else
            {
                g.Clip = new Region(new Rectangle(HoverTile.X * 32 - ViewPort.Left, HoverTile.Y * 32 - ViewPort.Top, 32, 32));
                g.Clear(Color.Transparent);
                DrawLayer((LayerData)lstLayers.SelectedItem, g, ViewPort, HoverTile);
            }

            picLayer.Image = img;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DrawMap((LayerData)lstLayers.SelectedItem);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DrawMap((LayerData)lstLayers.SelectedItem);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeSize_Click(object sender, EventArgs e)
        {
            UpdateMapSize((MapData)lstMaps.SelectedItem, int.Parse(txtMapWidth.Text), int.Parse(txtMapHeight.Text));

            ((MapData)lstMaps.SelectedItem).Width = int.Parse(txtMapWidth.Text);
            ((MapData)lstMaps.SelectedItem).Height = int.Parse(txtMapHeight.Text);
            DataManager.Update(Maps[1]);



            LoadSelectedMap();

        }

        private void UpdateMapSize(MapData map, int width, int height)
        {
            int oldWidth = map.Width;
            int oldHeight = map.Height;

            map.Width = width;
            map.Height = height;

            DataManager.BeginTransaction();

            DataManager.Update(map);
            DataManager.Resized(map, width, height, oldWidth, oldHeight);


            DataManager.Commit();

        }


        private Point MapTileAt(Point point)
        {
            return new Point(point.X / 32 + hscrMap.Value, point.Y / 32 + vscrMap.Value);
        }

        private void lstTileSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image img = TileSheets[int.Parse(lstTileSets.SelectedItem.ToString())];

            vscrTile.Value = 0;
            hscrTile.Value = 0;
            int vShowing = picTileSet.Height / 32;
            int hShowing = picTileSet.Width / 32;
            if (vShowing < img.Height / 32)
            {
                vscrTile.Visible = true;
                vscrTile.Maximum = (img.Height / 32) - vShowing;
            }
            else
                vscrTile.Visible = false;
            if (hShowing < img.Width / 32)
            {
                hscrTile.Visible = true;
                hscrTile.Maximum = (img.Width / 32) - hShowing;
            }
            else
                hscrTile.Visible = false;

            DrawTileset(img);
        }

        private void DrawTileset(Image tsImg)
        {
            Image picImage = new Bitmap(picTileSet.Width, picTileSet.Height);

            Graphics g = Graphics.FromImage(picImage);
            g.DrawImage(tsImg, new Rectangle(0, 0, picImage.Width, picImage.Height), new Rectangle(hscrTile.Value * 32, vscrTile.Value * 32, picImage.Width, picImage.Height), GraphicsUnit.Pixel);

            picTileSet.Image = picImage;
        }

        private void picTileSet_Click(object sender, EventArgs e)
        {
            
        }

        private void vscrTile_Scroll(object sender, ScrollEventArgs e)
        {
            DrawTileset(TileSheets[int.Parse(lstTileSets.SelectedItem.ToString())]);
        }

        private void hscrTile_Scroll(object sender, ScrollEventArgs e)
        {
            DrawTileset(TileSheets[int.Parse(lstTileSets.SelectedItem.ToString())]);
        }

        private void picTileSet_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedTile = TileAt(new Point(e.X, e.Y));

            SelectedSheet = int.Parse(lstTileSets.SelectedItem.ToString());

            Image tilePic = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(tilePic);

            g.DrawImage(TileSheets[SelectedSheet], new Rectangle(0,0,32,32), new Rectangle(SelectedTile.X * 32, SelectedTile.Y * 32, 32, 32), GraphicsUnit.Pixel);

            picTile.Image = tilePic;

            lblTile.Text = "Tile (" + SelectedTile.X + ", " + SelectedTile.Y + ") - Sheet " + SelectedSheet;
        }

        private void picTileSet_MouseMove(object sender, MouseEventArgs e)
        {
            HoverTile = TileAt(new Point(e.X, e.Y));

            lblTilePos.Text = "(" + HoverTile.X + "," + HoverTile.Y + ")";
        }


        private Point TileAt(Point point)
        {
            return new Point(point.X / 32 + hscrTile.Value, point.Y / 32 + vscrTile.Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataManager.BeginTransaction();
            foreach (MapData map in Maps.Values)
            {
                DataManager.Update(map, true);
            }
            DataManager.Commit();
        }

        private void PaintTile()
        {
            LayerData curLayer = (LayerData)lstLayers.SelectedItem;
            bool Painted = false;
            foreach (TileData tile in curLayer.Tiles)
            {
                if (tile.X == HoverTile.X && tile.Y == HoverTile.Y)
                {
                    if (tile.Sheet != SelectedSheet ||
                        tile.SheetX != SelectedTile.X ||
                        tile.SheetY != SelectedTile.Y)
                    {
                        tile.Sheet = SelectedSheet;
                        tile.SheetX = SelectedTile.X;
                        tile.SheetY = SelectedTile.Y;
                        Painted = true;
                    }
                    break;
                }
            }
            if (Painted)
            {
                //if (chkDrawAll.Checked)
                    DrawMap(HoverTile);
                //else
                //    DrawMap(curLayer, HoverTile);
            }

        }



        private void picLayer_MouseMove(object sender, MouseEventArgs e)
        {
            HoverTile = MapTileAt(new Point(e.X, e.Y));
            TileData tile = ((LayerData)lstLayers.SelectedItem).TileAt(HoverTile.X, HoverTile.Y); ;
            if (tile == null)
            {
                lblMapPos.Text = "";
                picMapTile.Visible = false;
                return;
            }
            picMapTile.Visible = true;
            lblMapPos.Text = "(" + HoverTile.X + "," + HoverTile.Y + ")";

            if (e.Button == MouseButtons.Left)
            {
                PaintTile();
            }
            else
            {
                Image tilePic = new Bitmap(32, 32);
                Graphics g = Graphics.FromImage(tilePic);

                g.DrawImage(TileSheets[tile.Sheet], new Rectangle(0, 0, 32, 32), new Rectangle(tile.SheetX * 32, tile.SheetY * 32, 32, 32), GraphicsUnit.Pixel);

                picMapTile.Image = tilePic;
            }
        }

        private void picLayer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (SelectedSheet == 0)
                    return;
                PaintTile();
            }
            else if (e.Button == MouseButtons.Right)
            {
                LayerData curLayer = (LayerData)lstLayers.SelectedItem;

                TileData selectedTile = null;

                do
                {
                    foreach (TileData tile in curLayer.Tiles)
                    {
                        if (tile.X == HoverTile.X && tile.Y == HoverTile.Y)
                        {
                            selectedTile = tile;
                            break;
                        }
                    }
                    if (selectedTile == null) //Outside of natural range
                        return;
                    if (selectedTile.Sheet == 1 && selectedTile.SheetX == 1 && selectedTile.SheetY == 1 && chkDrawAll.Checked) //Is the transparent Tile or if we can't see other layers
                    {
                        if (lstLayers.Items.IndexOf(curLayer) == 0)
                            break;
                        else
                        {
                            selectedTile = null;
                            curLayer = (LayerData)lstLayers.Items[lstLayers.Items.IndexOf(curLayer) - 1];
                        }
                    }
                } while (selectedTile == null);


                SelectedSheet = selectedTile.Sheet;
                SelectedTile.X = selectedTile.SheetX;
                SelectedTile.Y = selectedTile.SheetY;
                

                Image tilePic = new Bitmap(32, 32);
                Graphics g = Graphics.FromImage(tilePic);

                g.DrawImage(TileSheets[SelectedSheet], new Rectangle(0, 0, 32, 32), new Rectangle(SelectedTile.X * 32, SelectedTile.Y * 32, 32, 32), GraphicsUnit.Pixel);

                picTile.Image = tilePic;

                lblTile.Text = "Tile (" + SelectedTile.X + ", " + SelectedTile.Y + ") - Sheet " + SelectedSheet;

                if (e.Clicks > 1)
                {
                    lstTileSets.SelectedItem = SelectedSheet.ToString().PadLeft(3, '0');
                }
            }
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            MapData map = (MapData)lstMaps.SelectedItem;
            LayerData newLayer = new LayerData(DataManager.getNextLayerID(), "Layer: " + map.Layers.Count, map.ID, map.Layers.Count);
            map.Layers.Add(newLayer);

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    TileData tile = new TileData(x, y, newLayer.ID, 1, 1, 1);
                    newLayer.Tiles.Add(tile);
                }
            }

            //DataManager.BeginTransaction();
            //DataManager.Update(map, true);
            //DataManager.Commit();

            LoadSelectedMap();
            
        }

        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            MapData map = (MapData)lstMaps.SelectedItem;


            LayerData layer = (LayerData)lstLayers.SelectedItem;

            map.Layers.Remove(layer);
            DataManager.Delete(layer);

            LoadSelectedMap();
        }

        private void chkDrawAll_CheckedChanged(object sender, EventArgs e)
        {
            DrawMap();
        }

        private void picLayer_Click(object sender, EventArgs e)
        {

        }


    }
}
