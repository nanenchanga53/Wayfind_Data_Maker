using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WayFinderTool.Message;
using WayFinderTool.Model;

namespace WayFinderTool.ViewModel
{
    public class NodeDotViewModel : ViewModelBase
    {
        private NodeType _type;
        private Thickness _margin = new Thickness(0, 0, 0, 0);
        private double _x;
        private double _y;

        public string Map { get; set; }
        public string Parent { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public string Category { get; set; }
        public string IsUse { get; set; }
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                Set(ref _x, value);
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                Set(ref _y, value);
            }
        }
        public string Text { get; set; }
        public Brush Fill { get; private set; }
        public ICommand LeftClick { get; private set; }
        public ICommand RightClick { get; private set; }
        public NodeType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                Category = value.ToString();
                switch(value)
                {
                    case NodeType.Elevator:
                        Fill = Brushes.Pink;
                        break;
                    case NodeType.Escalator:
                        Fill = Brushes.Purple;
                        break;
                    case NodeType.Facility:
                        Fill = Brushes.Yellow;
                        break;
                    case NodeType.Kiosk:
                        Fill = Brushes.Green;
                        break;
                    case NodeType.Legend:
                        Fill = Brushes.Cyan;
                        break;
                    case NodeType.Stairs:
                        Fill = Brushes.Magenta;
                        break;
                    case NodeType.Map:
                        Fill = Brushes.DarkOrange;
                        break;
                    default:
                        Fill = Brushes.Red;
                        break;
                }
            }
        }
        public Thickness Margin
        {
            get
            {
                return _margin;
            }
            set
            {
                Set(ref _margin, value);
            }
        }

        public ObservableCollection<NodeDotViewModel> ConnectedNodes { get; set; }

        public NodeDotViewModel()
        {
            ConnectedNodes = new ObservableCollection<NodeDotViewModel>();
            LeftClick = new RelayCommand(() => ExecuteLeftClick());
            RightClick = new RelayCommand(() => ExecuteRightClick());
        }

        public void ChangeNodeAxis(double px, double py)
        {
            X = px;
            Y = py;
            Margin = new Thickness(px, py, 0, 0);
        }

        private void ExecuteLeftClick()
        {
            ModeType mode = MainViewModel.ActionControlVM.ModeStatus;

            if (mode == ModeType.NodeDelete)
            {
                Messenger.Default.Send<WorkSpaceClickMessage>(new WorkSpaceClickMessage() { Node = this, Px = X, Py = Y });
            }
            else if(mode == ModeType.EdgeAdd)
            {
                MainViewModel.WorkSpaceVM.SelectedNode = this;
            }
            else
            {
                Messenger.Default.Send<MouseLeftPressedMessage>(new MouseLeftPressedMessage() { Pressed = true });
                MainViewModel.WorkSpaceVM.SelectedNode = this;
            }
        }

        private void ExecuteRightClick()
        {
            ModeType mode = MainViewModel.ActionControlVM.ModeStatus;

            if (mode == ModeType.EdgeAdd)
            {
                Messenger.Default.Send<MouseRightClickMessage>(new MouseRightClickMessage() { Node = this });

            }
            else if( mode == ModeType.EdgeDel)
            {
                Messenger.Default.Send<MouseRightClickMessage>(new MouseRightClickMessage() { Node = this });
            }
        }
    }
}
