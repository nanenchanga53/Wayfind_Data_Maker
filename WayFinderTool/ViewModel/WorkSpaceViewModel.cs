using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WayFinderTool.Message;
using WayFinderTool.Model;
using WayFindToolCommon.Model;

namespace WayFinderTool.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WorkSpaceViewModel : ViewModelBase
    {
        private ObservableCollection<NodeDotViewModel> _nodePixels;
        private NodeDotViewModel _selectedNode;
        private bool _mousePressed = false;
        private ImageSource _baseMap;
        private ImageSource _overlayImage;

        public ObservableCollection<NodeDotViewModel> NodePixels
        {
            get
            {
                return _nodePixels;
            }
            set
            {
                Set(ref _nodePixels, value);
                RaisePropertyChanged(nameof(NodePixels));
            }
        }

        public ObservableCollection<NodeDotViewModel> EdgePixels
        {
            get
            {
                if (SelectedNode != null)
                    return SelectedNode.ConnectedNodes;
                else
                    return new ObservableCollection<NodeDotViewModel>();
            }
        }

        public NodeDotViewModel SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                Set(ref _selectedNode, value);
                Messenger.Default.Send(new DisplayNodeInfo { NodeInfo = value });
                RaisePropertyChanged(nameof(EdgePixels));
            }
        }

        public MapDataModel GetData
        {
            get
            {
                MapDataModel result = new MapDataModel();
                result.Nodes = new List<Node>();
                result.Edges = new List<Edge>();

                foreach(NodeDotViewModel element in NodePixels)
                {
                    Node node = new Node();
                    node.NOD_CATEGORY = element.Category;
                    node.NOD_FLOOR = element.Floor;
                    node.NOD_ID = element.ID;
                    node.NOD_ISUSE = element.IsUse == "True" || element.IsUse == "1" ? true : false;
                    node.NOD_NAME = element.Name;
                    node.Parent = element.Parent;
                    node.NOD_X = element.X;
                    node.NOD_Y = element.Y;
                    node.MAP_ID = element.Map;

                    foreach(NodeDotViewModel edge_element in element.ConnectedNodes)
                    {
                        
                        Edge edge = new Edge();
                        edge.EDG_NODE1 = element.ID;
                        edge.EDG_NODE2 = edge_element.ID;
                        edge.MMT_ID = string.Empty;
                        
                        result.Edges.Add(edge);
                    }

                    result.Nodes.Add(node);
                }

                return result;
            }
        }

        public bool MousePressed
        {
            get
            {
                return _mousePressed;
            }
            set
            {
                Set(ref _mousePressed, value);
            }
        }

        public NodeDotViewModel PrevSelectedNode { get; set; }

        public ImageSource BaseMap
        {
            get
            {
                return _baseMap;
            }

            set
            {
                Set(nameof(BaseMap), ref _baseMap, value);
            }
        }

        public ImageSource OverlayImage
        {
            get
            {
                return _overlayImage;
            }

            set
            {
                Set(nameof(OverlayImage), ref _overlayImage, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the WorkSpaceViewModel class.
        /// </summary>
        public WorkSpaceViewModel()
        {
            _nodePixels = new ObservableCollection<NodeDotViewModel>();

            Messenger.Default.Register<WorkSpaceDoubleClickMessage>(this, x => WorkSpaceDoubleClick(x.Px, x.Py));
            Messenger.Default.Register<WorkSpaceClickMessage>(this, x => WorkSpaceClick(x.Node, x.Px, x.Py));
            Messenger.Default.Register<MouseMoveMessage>(this, x => MouseMove(x.Px, x.Py));
            Messenger.Default.Register<MouseLeftPressedMessage>(this, x => MousePressed = x.Pressed);
            Messenger.Default.Register<MouseRightClickMessage>(this, x => MouseRightClick(x.Node));

        }

        public void WorkSpaceDoubleClick(double posX, double posY)
        {
            double px = posX - (posX % 8);
            double py = posY - (posY % 8);

            px = px < 0 ? 0 : px;
            py = py < 0 ? 0 : py;

            NodeDotViewModel t = new NodeDotViewModel()
            {
                X = px,
                Y = py,
                ID = Guid.NewGuid().ToString(),
                Type = Model.NodeType.Normal,
                Margin = new Thickness(px, py, 0, 0),
                Floor = MainViewModel.ActionControlVM.FloorString,
                Map = MainViewModel.ActionControlVM.MapString,
                IsUse = "1",
            };
            t.Text = t.ID;

            switch (MainViewModel.ActionControlVM.ModeStatus)
            {
                case ModeType.NodeElevator:
                    t.Type = NodeType.Elevator;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
                case ModeType.NodeEscalator:
                    t.Type = NodeType.Escalator;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
                case ModeType.NodeFacility:
                    t.Type = NodeType.Facility;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
                case ModeType.NodeKiosk:
                    t.Type = NodeType.Kiosk;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
                case ModeType.NodeLegend:
                    t.Type = NodeType.Legend;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
                case ModeType.NodeNormal:
                    t.Type = NodeType.Normal;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
                case ModeType.NodeStairs:
                    t.Type = NodeType.Stairs;

                    if (NodePixels.Select(x => x).Where(y => y.ID == t.ID).Count() == 0)
                        NodePixels.Add(t);
                    break;
            }
        }

        public void WorkSpaceClick(NodeDotViewModel node, double posX, double posY)
        {
            double px = posX - (posX % 8);
            double py = posY - (posY % 8);

            px = px < 0 ? 0 : px;
            py = py < 0 ? 0 : py;

            NodeDotViewModel t = new NodeDotViewModel()
            {
                X = px,
                Y = py,
                ID = Guid.NewGuid().ToString(),
                Type = Model.NodeType.Normal,
                Margin = new Thickness(px, py, 0, 0),
                Floor = MainViewModel.ActionControlVM.FloorString,
                Map = MainViewModel.ActionControlVM.MapString,
                IsUse = "1",
            };
            t.Text = t.ID;

            switch (MainViewModel.ActionControlVM.ModeStatus)
            {
                case ModeType.EdgeAdd:
                    break;
                case ModeType.EdgeDel:
                    break;
                case ModeType.NodeDelete:
                    if (NodePixels.Where(x => x.ID == node.ID).Count() == 1)
                        NodePixels.Remove(NodePixels.Where(x => x.ID == node.ID).Single());
                    break;
            }
        }

        public void MouseMove(double posX, double posY)
        {
            double px = posX - (posX % 8);
            double py = posY - (posY % 8);

            px = px < 0 ? 0 : px;
            py = py < 0 ? 0 : py;

            if (SelectedNode != null && MousePressed)
            {
                if(MainViewModel.ActionControlVM.ModeStatus != ModeType.NodeDelete || MainViewModel.ActionControlVM.ModeStatus != ModeType.EdgeDel || MainViewModel.ActionControlVM.ModeStatus != ModeType.EdgeAdd)
                {
                    SelectedNode.ChangeNodeAxis(px, py);
                    //NodePixels.Where(x => x.ID == SelectedNode.ID).Single().ChangeNodeAxis(px, py);
                    RaisePropertyChanged(nameof(NodePixels));
                }
            }
        }

        public void MouseRightClick(NodeDotViewModel node)
        {
            if(SelectedNode != null)
            {
                switch(MainViewModel.ActionControlVM.ModeStatus)
                {
                    case ModeType.EdgeAdd:
                        if(NodePixels.Where(x => x.ID == SelectedNode.ID).Single().ConnectedNodes.Where(y=>y.ID == node.ID).Count() == 0)
                        {
                            //SelectedNode.ConnectedNodes.Add(node);
                            NodePixels.Where(x => x.ID == SelectedNode.ID).Single().ConnectedNodes.Add(node);
                        }
                        break;
                    case ModeType.EdgeDel:
                        NodePixels.Where(x => x.ID == node.ID).Single().ConnectedNodes.Remove(node);
                        SelectedNode.ConnectedNodes.Remove(node);
                        RaisePropertyChanged(nameof(NodePixels));
                        RaisePropertyChanged(nameof(EdgePixels));
                        //SelectedNode.ConnectedNodes.Where(x => x.ID == node.ID).Select(x => SelectedNode.ConnectedNodes.Remove(x));
                        //NodePixels.Where(x => x.ID == node.ID).Select(x => NodePixels.Remove(x));
                        break;
                }
                RaisePropertyChanged(nameof(EdgePixels));
            }
        }
    }
}