using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace RedSkyData
{
    public static class DataManager
    {
        private static string Path;
        private static SQLiteConnection connection;
        private static SQLiteCommand command;
        private static SQLiteTransaction transaction;

        public static void Connect(string path)
        {
            Path = path;
            connection = new SQLiteConnection("data source=" + Path);
            command = new SQLiteCommand(connection);
         
            connection.Open();
        }

        public static void Close()
        {
            connection.Close();
        }

        public static void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
        }

        public static void Commit()
        {
            transaction.Commit();
            command.Transaction = null;
        }

        public static void NonQuery(string query)
        {

            command.CommandText = query;
            command.ExecuteNonQuery();      // Execute the query
        }

        public static DataTable Query(string query)
        {
            DataTable dt = new DataTable();
            

            command.CommandText = query;      // Select all rows from our database table

            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            da.Fill(dt);

            return dt;
        }

        //public List<MapData> LoadMaps()
        //{
        //    List<MapData> maps = new List<MapData>();
        //    using (SQLiteConnection con = new SQLiteConnection("data source=" + Path))
        //    {
        //        using (SQLiteCommand com = new SQLiteCommand(con))
        //        {
        //            con.Open();

        //            com.CommandText = "Select * FROM Map";      // Select all rows from our database table

        //            using (SQLiteDataReader reader = com.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    maps.Add(new MapData(reader));
        //                }
        //            }
        //            con.Close();        // Close the connection to the database

        //        }
        //    }
        //    return maps;
        //}

        public static List<MapData> LoadMaps()
        {
            List<MapData> maps = new List<MapData>();

            foreach (DataRow row in Query("Select * From Map").Rows)
                maps.Add(new MapData(row));

            return maps;
        }

        public static List<LayerData> LoadLayers(int Map)
        {
            List<LayerData> layers = new List<LayerData>();

            foreach (DataRow row in Query("Select * From MapLayer Where Map = " + Map).Rows)
                layers.Add(new LayerData(row));

            return layers;
        }

        public static List<TileData> LoadTiles(int Layer)
        {
            List<TileData> tiles = new List<TileData>();

            foreach (DataRow row in Query("Select * From LayerTile Where Layer = " + Layer).Rows)
                tiles.Add(new TileData(row));

            return tiles;
        }

        public static void Update(TileData t)
        {
            if (Query("Select * From LayerTile Where Layer = " + t.Layer + " AND X = " + t.X + " AND Y = " + t.Y).Rows.Count > 0)
                NonQuery("Update LayerTile Set Sheet = " + t.Sheet + ", SheetX = " + t.SheetX + ", SheetY = " + t.SheetY +
                     " Where Layer = " + t.Layer + " AND X = " + t.X + " AND Y = " + t.Y);
            else
                NonQuery("INSERT INTO LayerTile (Layer, X, Y, Sheet, SheetX, SheetY) VALUES (" +
                    t.Layer + "," + t.X + "," + t.Y + "," + t.Sheet + "," + t.SheetX + "," + t.SheetY + ")");

        }

        public static void Update(MapData m, bool deep = false)
        {
            if (Query("Select * From Map Where ID = " + m.ID).Rows.Count > 0)
                NonQuery("Update Map Set Name = '" + m.Name + "', Width = " + m.Width + ", Height = " + m.Height +
                     " Where ID = " + m.ID);
            else
                NonQuery("INSERT INTO Map (ID, Name, Width, Height) VALUES (" +
                    m.ID + ",'" + m.Name + "', " + m.Width + ", " + m.Height + ")");

            if (deep)
            {
                foreach (LayerData layer in m.Layers)
                {
                    Update(layer, true);
                }
            }

        }

        private static void Update(LayerData l, bool deep = false)
        {
            if (Query("Select * From MapLayer Where ID = " + l.ID).Rows.Count > 0)
                NonQuery("Update MapLayer Set Name = '" + l.Name + "', Map = " + l.Map+ ", [Order] = " + l.Order +
                     " Where ID = " + l.ID);
            else
                NonQuery("INSERT INTO MapLayer (ID, Name, Map, [Order]) VALUES (" +
                    l.ID + ",'" + l.Name + "', " + l.Map + ", " + l.Order + ")");

            if (deep)
            {
                foreach (TileData tile in l.Tiles)
                {
                    Update(tile);
                }
            }
        }

        public static int getNextLayerID()
        {
            return int.Parse(Query("SELECT MAX(ID) FROM MapLayer").Rows[0][0].ToString()) + 1;
        }

        public static void Delete(LayerData layer)
        {
            NonQuery("Delete From LayerTile Where Layer = " + layer.ID);
            NonQuery("Delete From MapLayer Where ID = " + layer.ID);
        }

        public static void Resized(MapData map, int width, int height, int oldWidth, int oldHeight)
        {
            if (oldWidth > map.Width || oldHeight > map.Height)
            {
                foreach (LayerData layer in map.Layers)
                {
                    NonQuery("Delete From LayerTile Where X >= " + map.Width + " OR Y >= " + map.Height);
                }
            }
            if (oldWidth < map.Width || oldHeight < map.Height)
            {
                foreach (LayerData layer in map.Layers)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        for (int y = 0; y < map.Height; y++)
                        {
                            if (x >= oldWidth || y >= oldHeight)

                                NonQuery("INSERT INTO LayerTile (Layer, X, Y, Sheet, SheetX, SheetY) VALUES (" +
                                        layer.ID + "," + x + "," + y + ",1,1,1)");
                        }
                    }

                }
            }
        }
    }
}
