using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSkyData
{
    public class LayerData
    {
        public int ID;
        public string Name;
        public int Map;
        public int Order;
        public List<TileData> Tiles;

        public LayerData(System.Data.DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Name = row["Name"].ToString();
            Map = int.Parse(row["Map"].ToString());
            Order = int.Parse(row["Order"].ToString());

            Tiles = DataManager.LoadTiles(ID);
        }

        public LayerData(int id, string name, int map, int order)
        {
            ID = id;
            Name = name;
            Map = map;
            Order = order;

            Tiles = new List<TileData>();
        }

        public override string ToString()
        {
            return (Name == "" ? "" : Name + ": ") + "Layer " + Order + " of Map " + Map;
        }

        public TileData TileAt(int x, int y)
        {
            foreach (TileData tile in Tiles)
            {
                if (tile.X == x && tile.Y == y)
                    return tile;
            }
            return null;
        }
    }
}
