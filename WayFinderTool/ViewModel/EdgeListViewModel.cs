using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WayFindToolCommon.Model;

namespace WayFinderTool.ViewModel
{
    public class EdgeListViewModel : ViewModelBase
    {
        private ObservableCollection<Edge> _edgeCollection = new ObservableCollection<Edge>();
        private string _modeToggleTitle = "Change to EdgeMode";
        private RelayCommand<object> _buttonDeleteEdge = null;

        public ObservableCollection<Edge> EdgeCollection
        {
            get
            {
                return _edgeCollection;
            }
            set
            {
                Set(ref _edgeCollection, value);
            }
        }

        public string ModeToggleTitle
        {
            get
            {
                return _modeToggleTitle;
            }
            set
            {
                Set(ref _modeToggleTitle, value);
            }
        }

        public ICommand ButtonChangeMode { get; set; }
        public ICommand ButtonModifyApply { get; set; }
        public ICommand ButtonDeleteEdge { get; set; }
        //public RelayCommand<object> ButtonDeleteEdge
        //{
        //    get
        //    {
        //        if(_buttonDeleteEdge == null)
        //        {
        //            //_buttonDeleteEdge = new RelayCommand<object>(param => ButtonDeleteEdgeExecute());
        //        }
        //
        //        return _buttonDeleteEdge;
        //    }
        //}

        /// <summary>
        /// Initializes a new instance of the EdgeListViewModel class.
        /// </summary>
        public EdgeListViewModel()
        {
            ButtonChangeMode = new RelayCommand(ButtonChangeModeExecute);
            ButtonModifyApply = new RelayCommand(ButtonModifyApplyExecute);
            ButtonDeleteEdge = new RelayCommand(ButtonDeleteEdgeExecute);
        }

        public void ButtonChangeModeExecute()
        {
            //MainWindowViewModel._nodeToolViewModel.EdgeMode = !MainWindowViewModel._nodeToolViewModel.EdgeMode;
            //bool edgeMode = MainWindowViewModel._nodeToolViewModel.EdgeMode;
            //if (edgeMode == true)
            //{
            //    ModeToggleTitle = "Change to NodeMode";
            //}
            //else
            //{
            //    ModeToggleTitle = "Change to EdgeMode";
            //}
        }

        public void LoadConnectedEdge(Node node)
        {
            //if (MainWindowViewModel._nodeToolViewModel.EdgeMode == false)
            //{
            //    EdgeCollection = new ObservableCollection<Edge>(MapDataList.Instance.MapData.Edges.Where(x => x.Node1 == node.Id).ToList<Edge>());
            //
            //}

        }

        public void AddEdge(string node1, string node2)
        {
            EdgeCollection.Add(new Edge { EDG_NODE1 = node1, EDG_NODE2 = node2, EDG_LENGTH = 1, EDG_WEIGTH = 1 });
        }

        public void AddEdge(string node1, string node2, int length, int weigth)
        {
            EdgeCollection.Add(new Edge { EDG_NODE1 = node1, EDG_NODE2 = node2, EDG_LENGTH = length, EDG_WEIGTH = weigth });
        }

        public void ButtonModifyApplyExecute()
        {
            //if (MainWindowViewModel._nodeToolViewModel.EdgeMode)
            //{
            //    foreach (var t in EdgeCollection)
            //    {
            //
            //        if (MapDataList.Instance.MapData.Edges.Where(x => x.Node1 == t.Node1 && x.Node2 == t.Node2).Count() > 0)
            //            MapDataList.Instance.MapData.Edges.RemoveAll(x => x.Node1 == t.Node1 && x.Node2 == t.Node2);
            //        MapDataList.Instance.MapData.Edges.Add(new Edge { Node1 = t.Node1, Node2 = t.Node2, Length = t.Length, Weight = t.Weight });
            //    }
            //    ButtonChangeModeExecute();
            //}
        }

        public void ButtonDeleteEdgeExecute()
        {
            //MessageBox.Show(node as string);
            //MessageBox.Show("!!");
        }
    }
}
