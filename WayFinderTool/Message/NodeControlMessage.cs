using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayFinderTool.Model;
using WayFinderTool.ViewModel;

namespace WayFinderTool.Message
{
    class NodeControlMessage
    {
    }

    public class WorkSpaceDoubleClickMessage
    {
        public double Px { get; set; }
        public double Py { get; set; }
    }

    public class WorkSpaceClickMessage
    {
        public NodeDotViewModel Node { get; set; }
        public double Px { get; set; }
        public double Py { get; set; }
    }

    public class MouseMoveMessage
    {
        public double Px { get; set; }
        public double Py { get; set; }
    }
    
    public class MouseLeftPressedMessage
    {
        public bool Pressed { get; set; }
    }

    public class MouseRightClickMessage
    {
        public NodeDotViewModel Node { get; set; }
    }
}
