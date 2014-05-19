using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedSkyData
{
    public class TileData
    {
        public int X;
        public int Y;
        public int Layer;
        public int Sheet;
        public int SheetX;
        public int SheetY;
        private int x;
        private int y;
        private int layer;
        private int p1;
        private int p2;
        private int p3;

        public TileData(System.Data.DataRow row)
        {
            X = int.Parse(row["X"].ToString());
            Y = int.Parse(row["Y"].ToString());
            Layer = int.Parse(row["Layer"].ToString());
            Sheet = int.Parse(row["Sheet"].ToString());
            SheetX = int.Parse(row["SheetX"].ToString());
            SheetY = int.Parse(row["SheetY"].ToString());
        }

        public TileData(int x, int y, int layer, int sheet, int sheetx, int sheety)
        {
            X = x;
            Y = y;
            Layer = layer;
            Sheet = sheet;
            SheetX = sheetx;
            SheetY = sheety;
        }

        public override string ToString()
        {
            return "Tile (" + X +"," + Y + ") on Layer " + Layer + " - [" + Sheet + ": " + SheetX + "," + SheetY + "]";
        }
    }
}
