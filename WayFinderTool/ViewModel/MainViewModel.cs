using GalaSoft.MvvmLight;
using WayFinderTool.Model;

namespace WayFinderTool.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public readonly static WorkSpaceViewModel WorkSpaceVM = new WorkSpaceViewModel();
        public readonly static NodeInfoViewModel NodeInfoVM = new NodeInfoViewModel();
        public readonly static ActionControlViewModel ActionControlVM = new ActionControlViewModel();
        public readonly static MenuControlViewModel MenuControlVM = new MenuControlViewModel();
        public readonly static EdgeListViewModel EdgeListVM = new EdgeListViewModel();

        private ViewModelBase _content_WorkSpace;
        private ViewModelBase _content_NodeInfo;
        private ViewModelBase _content_ActionControl;
        private ViewModelBase _content_MenuControl;
        private ViewModelBase _content_EdgeList;

        public ViewModelBase Content_EdgeList
        {
            get
            {
                return _content_EdgeList;
            }
            set
            {
                Set(ref _content_EdgeList, value);
            }
        }

        public ViewModelBase Content_WorkSpace
        {
            get
            {
                return _content_WorkSpace;
            }
            set
            {
                Set(ref _content_WorkSpace, value);
            }
        }

        public ViewModelBase Content_NodeInfo
        {
            get
            {
                return _content_NodeInfo;
            }
            set
            {
                Set(ref _content_NodeInfo, value);
            }
        }

        public ViewModelBase Content_ActionControl
        {
            get
            {
                return _content_ActionControl;
            }
            set
            {
                Set(ref _content_ActionControl, value);
            }
        }

        public ViewModelBase Content_MenuControl
        {
            get
            {
                return _content_MenuControl;
            }
            set
            {
                Set(ref _content_MenuControl, value);
            }
        }

        public MainViewModel()
        {
            Content_WorkSpace = WorkSpaceVM;
            Content_NodeInfo = NodeInfoVM;
            Content_ActionControl = ActionControlVM;
            Content_MenuControl = MenuControlVM;
            Content_EdgeList = EdgeListVM;
        }
    }
}