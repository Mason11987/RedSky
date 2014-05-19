using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSkyData
{
    public class MapData
    {
        public int ID;
        public string Name;
        public int Width;
        public int Height;
        public List<LayerData> Layers;

        public MapData(System.Data.DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Name = row["Name"].ToString();
            Width = int.Parse(row["Width"].ToString());
            Height = int.Parse(row["Height"].ToString());

            Layers = DataManager.LoadLayers(ID);
        }

        public override string ToString()
        {

            return Name;
        }
    }
}
