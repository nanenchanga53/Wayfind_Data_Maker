using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using WayFindToolCommon.Core;
using WayFindToolCommon.Model;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using WayFinderTool.Utility;
using WayFinderTool.Model;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace WayFinderTool.ViewModel
{
    enum FileType
    {
        json = 0,
        png = 1
    }

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MenuControlViewModel : ViewModelBase
    {
        public ICommand ButtonGoCenter { get; set; }
        public ICommand ButtonDataLoad { get; set; }
        public ICommand ButtonDataSave { get; set; }
        public ICommand ButtonImageLoad { get; set; }
        public ICommand ButtonOverlayImageLoad { get; set; }

        /// <summary>
        /// Initializes a new instance of the MenuControlViewModel class.
        /// </summary>
        public MenuControlViewModel()
        {
            ButtonGoCenter = new RelayCommand(ButtonGoCenterExecute);
            ButtonDataLoad = new RelayCommand(ButtonDataLoadExecute);
            ButtonDataSave = new RelayCommand(ButtonDataSaveExecute);
            ButtonImageLoad = new RelayCommand(ButtonImageLoadExecute);
            ButtonOverlayImageLoad = new RelayCommand(ButtonOverlayImageLoadExecute);
        }

        public void ButtonGoCenterExecute()
        {

        }

        public void ButtonDataLoadExecute()
        {
            string filePath = ShowDialog((int)FileType.json);
            if (filePath != string.Empty)
            {
                MapDataList mapDataList = MapDataList.Instance;
                if(mapDataList.MapData.Nodes.Count() != 0 || mapDataList.MapData.Edges.Count() != 0)
                {
                    if(MessageBox.Show("작업중인 데이터가 있습니다. 저장되지 않은 데이터는 유실 될 수 있습니다\r\n계속 진행하시겠습니까?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DataLoadProcess(filePath);
                    }
                }
                else
                {
                    DataLoadProcess(filePath);
                }
            }
        }

        public void ButtonDataSaveExecute()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.Filter = "Json Files(.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                DataHandler dataHandler = new DataHandler();
                dataHandler.Save(MainViewModel.WorkSpaceVM.GetData, saveFileDialog.FileName);
            }
        }
        public void ButtonImageLoadExecute()
        {
            string filePath = ShowDialog((int)FileType.png);
            if (filePath != string.Empty)
            {
                MainViewModel.WorkSpaceVM.BaseMap = new BitmapImage(new Uri(filePath));
                //MainWindowViewModel._nodeToolViewModel.BaseImage = new BitmapImage(new Uri(filePath));
            }
        }

        public void ButtonOverlayImageLoadExecute()
        {
            string filePath = ShowDialog((int)FileType.png);
            if (filePath != string.Empty)
            {
                MainViewModel.WorkSpaceVM.OverlayImage = new BitmapImage(new Uri(filePath));
                //MainWindowViewModel._nodeToolViewModel.BaseImage = new BitmapImage(new Uri(filePath));
            }
        }

        public string ShowDialog(int fileType)
        {
            string result = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            switch (fileType)
            {
                case (int)FileType.json:
                    openFileDialog.DefaultExt = ".json";
                    openFileDialog.Filter = "Json Files(.json)|*.json";
                    break;
                case (int)FileType.png:
                    openFileDialog.DefaultExt = ".png";
                    openFileDialog.Filter = "PNG Image Files(.png)|*.png";
                    break;
            }
            //Nullable<bool> fileResult = openFileDialog.ShowDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                if (new FileInfo(openFileDialog.FileName).Exists)
                {
                    result = openFileDialog.FileName;
                }
            }

            return result;
        }

        public void DataLoadProcess(string filePath)
        {
            MapDataList mapDataList = MapDataList.Instance;
            mapDataList.MapData.Nodes.Clear();
            mapDataList.MapData.Edges.Clear();
            MainViewModel.WorkSpaceVM.NodePixels.Clear();
            MainViewModel.WorkSpaceVM.EdgePixels.Clear();
            mapDataList.Load(filePath);

            if (mapDataList != null && mapDataList.MapData.Nodes != null && mapDataList.MapData.Edges != null)
            {
                foreach (Node nodeData in mapDataList.MapData.Nodes)
                {
                    NodeDotViewModel t = new NodeDotViewModel()
                    {
                        X = nodeData.NOD_X,
                        Y = nodeData.NOD_Y,
                        ID = nodeData.NOD_ID,
                        Text = nodeData.NOD_ID,
                        Margin = new Thickness(nodeData.NOD_X, nodeData.NOD_Y, 0, 0),
                        Floor = nodeData.NOD_FLOOR,
                        IsUse = nodeData.NOD_ISUSE == true ? "1" : "0",
                        Map = nodeData.MAP_ID
                    };

                    if(nodeData.NOD_CATEGORY != null)
                    {
                        try
                        {
                            t.Type = (NodeType)Enum.Parse(typeof(NodeType), nodeData.NOD_CATEGORY, true);
                        }
                        catch
                        {
                            t.Type = NodeType.Normal;
                        }
                    }
                    else
                    {
                        t.Type = NodeType.Normal;
                    }
                    

                    MainViewModel.WorkSpaceVM.NodePixels.Add(t);

                }

                foreach (Edge edgeData in mapDataList.MapData.Edges)
                {
                    MainViewModel.WorkSpaceVM.NodePixels.Where(x => x.ID == edgeData.EDG_NODE1).Single().ConnectedNodes.Add(MainViewModel.WorkSpaceVM.NodePixels.Where(y => y.ID == edgeData.EDG_NODE2).Single());
                }
            }
            else
            {
                MessageBox.Show("파일을 읽는중 오류가 발생하였습니다.");
            }
        }
    }
}