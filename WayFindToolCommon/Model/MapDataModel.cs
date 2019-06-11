using System.Collections.Generic;

namespace WayFindToolCommon.Model
{
    public class MapDataModel
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
    }

    public class Node
    {
        public string Parent { get; set; }
        public string NOD_ID { get; set; }
        public double NOD_X { get; set; }
        public double NOD_Y { get; set; }
        public string NOD_FLOOR { get; set; }
        public string NOD_NAME { get; set; }
        public string NOD_CATEGORY { get; set; }
        public bool NOD_ISUSE { get; set; }
        public string MAP_ID { get; set; }

        public Node Clone(Node node)
        {
            Node result = new Node();
            result.Parent = node.Parent;
            result.NOD_ID = node.NOD_ID;
            result.NOD_X = node.NOD_X;
            result.NOD_Y = node.NOD_Y;
            result.NOD_FLOOR = node.NOD_FLOOR;
            result.NOD_NAME = node.NOD_NAME;
            result.NOD_CATEGORY = node.NOD_CATEGORY;
            result.NOD_ISUSE = node.NOD_ISUSE;
            result.MAP_ID = node.MAP_ID;

            return result;
        }
    }

    public class Edge
    {
        public string MMT_ID { get; set; }
        public string EDG_NODE1 { get; set; }
        public string EDG_NODE2 { get; set; }
        public double EDG_LENGTH { get; set; }
        public double EDG_WEIGTH { get; set; }

        public double EDG_ISUSE { get; set; } = 1;

    }
}
