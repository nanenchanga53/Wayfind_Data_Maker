using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using WayFinderTool.Model;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using WayFinderTool.Message;

namespace WayFinderTool.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ActionControlViewModel : ViewModelBase
    {

        private string _floorString = string.Empty;
        private string _modeString = string.Empty;
        private ModeType _modeStatus = ModeType.NodeNormal;
        private string _mapString = string.Empty;
        public string FloorString
        {
            get
            {
                return _floorString;
            }
            set
            {
                Set(ref _floorString, value);
            }
        }

        public string MapString
        {
            get
            {
                return _mapString;
            }
            set
            {
                Set(ref _mapString, value);
            }
        }


        public string ModeString
        {
            get
            {
                return _modeString;
            }
            private set
            {
                Set(ref _modeString, value);
            }
        }

        public ModeType ModeStatus
        {
            get
            {
                return _modeStatus;
            }
            set
            {
                Set(ref _modeStatus, value);
                ModeString = value.ToString();
            }
        }

        public ICommand ButtonFloorStringSet { get; set; }
        public ICommand ButtonNodeNormal { get; set; }
        public ICommand ButtonNodeKiosk { get; set; }
        public ICommand ButtonNodeFacility { get; set; }
        public ICommand ButtonNodeLegend { get; set; }
        public ICommand ButtonNodeElevator { get; set; }
        public ICommand ButtonNodeEscalator { get; set; }
        public ICommand ButtonNodeStairs { get; set; }
        public ICommand ButtonNodeDelete { get; set; }

        public ICommand ButtonEdgeAdd { get; set; }
        public ICommand ButtonEdgeDel { get; set; }


        /// <summary>
        /// Initializes a = new instance of the ActionControlViewModel class.
        /// </summary>
        public ActionControlViewModel()
        {
            ButtonFloorStringSet = new RelayCommand(ButtonFloorStringSetExecute);

            ButtonNodeNormal = new RelayCommand(ButtonNodeNormalExecute);
            ButtonNodeKiosk = new RelayCommand(ButtonNodeKioskExecute);
            ButtonNodeFacility = new RelayCommand(ButtonNodeFacilityExecute);
            ButtonNodeLegend = new RelayCommand(ButtonNodeLegendExecute);
            ButtonNodeElevator = new RelayCommand(ButtonNodeElevatorExecute);
            ButtonNodeEscalator = new RelayCommand(ButtonNodeEscalatorExecute);
            ButtonNodeStairs = new RelayCommand(ButtonNodeStairsExecute);
            ButtonNodeDelete = new RelayCommand(ButtonNodeDeleteExecute);

            ButtonEdgeAdd = new RelayCommand(ButtonEdgeAddExecute);
            ButtonEdgeDel = new RelayCommand(ButtonEdgeDelExecute);

            ModeStatus = ModeType.NodeNormal;
        }

        public void ButtonFloorStringSetExecute()
        {
            
        }

        public void ButtonNodeNormalExecute()
        {
            ModeStatus = ModeType.NodeNormal;
        }
        public void ButtonNodeKioskExecute()
        {
            ModeStatus = ModeType.NodeKiosk;
        }
        public void ButtonNodeFacilityExecute()
        {
            ModeStatus = ModeType.NodeFacility;
        }
        public void ButtonNodeLegendExecute()
        {
            ModeStatus = ModeType.NodeLegend;
        }
        public void ButtonNodeElevatorExecute()
        {
            ModeStatus = ModeType.NodeElevator;
        }
        public void ButtonNodeEscalatorExecute()
        {
            ModeStatus = ModeType.NodeEscalator;
        }
        public void ButtonNodeStairsExecute()
        {
            ModeStatus = ModeType.NodeStairs;
        }
        public void ButtonNodeDeleteExecute()
        {
            ModeStatus = ModeType.NodeDelete;
        }
        public void ButtonEdgeAddExecute()
        {
            ModeStatus = ModeType.EdgeAdd;
        }
        public void ButtonEdgeDelExecute()
        {
            ModeStatus = ModeType.EdgeDel;
        }
    }
}