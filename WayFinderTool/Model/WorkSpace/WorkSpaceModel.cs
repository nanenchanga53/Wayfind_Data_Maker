using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace WayFinderTool.Model
{
    public enum NodeType
    {
        Normal = 0,
        Kiosk,
        Facility,
        Legend,
        Elevator,
        Escalator,
        Stairs,
        Map
    };

    public enum ModeType
    {
        NodeNormal = 0,
        NodeKiosk,
        NodeFacility,
        NodeLegend,
        NodeElevator,
        NodeEscalator,
        NodeStairs,
        NodeDelete,
        EdgeAdd,
        EdgeDel
    };

    public class WorkSpaceModel
    {

    }
}
