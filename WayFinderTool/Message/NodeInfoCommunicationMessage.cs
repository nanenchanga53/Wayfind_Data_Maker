using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayFinderTool.ViewModel;

namespace WayFinderTool.Message
{
    public class NodeInfoCommunicationMessage
    {
    }

    public class DisplayNodeInfo
    {
        public NodeDotViewModel NodeInfo { get; set; }
    }
}
