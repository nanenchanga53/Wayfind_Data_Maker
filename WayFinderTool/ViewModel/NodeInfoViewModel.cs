using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using WayFinderTool.Message;

namespace WayFinderTool.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class NodeInfoViewModel : ViewModelBase
    {
        public NodeDotViewModel _nodeInfo = new NodeDotViewModel();
        public NodeDotViewModel NodeInfo
        {
            get
            {
                return _nodeInfo;
            }
            set
            {
                if(value == null)
                {
                    value = new NodeDotViewModel();
                }
                Set(ref _nodeInfo, value);
                RaisePropertyChanged(nameof(NodeInfo));
            }
        }

        public ICommand ButtonModify { get; set; }
        public ICommand ButtonDelete { get; set; }

        /// <summary>
        /// Initializes a new instance of the NodeInfoViewModel class.
        /// </summary>
        public NodeInfoViewModel()
        {
            ButtonModify = new RelayCommand(ButtonModifyExecute);
            ButtonDelete = new RelayCommand(ButtonDeleteExecute);
            Messenger.Default.Register<DisplayNodeInfo>(this, x => NodeInfo = x.NodeInfo);
        }

        public void ButtonModifyExecute()
        {

        }

        public void ButtonDeleteExecute()
        {

        }
    }
}