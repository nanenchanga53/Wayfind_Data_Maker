using System.Collections.Generic;
using WayFindToolCommon.Model;

namespace WayFindToolCommon.Core
{
    public class MapDataList
    {
        private static MapDataList _instance;

        public static MapDataList Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MapDataList();
                }
                return _instance;
            }
        }

        public MapDataModel MapData { get; private set; }

        private MapDataList()
        {
            MapData = new MapDataModel();
            MapData.Nodes = new List<Node>();
            MapData.Edges = new List<Edge>();
        }

        public void Load(string fullPath)
        {
            MapData = new MapDataIO().Load(fullPath);
        }

        public void AddNode(Node node)
        {
            MapData.Nodes.Add(node);
        }

        public void ClearAll()
        {
            MapData.Edges.Clear();
            MapData.Nodes.Clear();
        }

        public void DeleteNode(Node node)
        {
            MapData.Nodes.Remove(node);
        }
    }
}
