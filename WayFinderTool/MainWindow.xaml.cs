using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using WayFinderTool.Message;
using WayFinderTool.ViewModel;
using System;

namespace WayFinderTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {
                Messenger.Default.Register<GetWorkSpaceActualSizeMessage>(this, x => GetWorkSpaceActualSize());
            };

            Unloaded += delegate
            {
                Messenger.Default.Unregister<GetWorkSpaceActualSizeMessage>(this);
            };

        }
        private void GetWorkSpaceActualSize()
        {
            Messenger.Default.Send(new SetWorkSpaceActualSizeMessage { Width = UI_ContentControl_WorkSpace.ActualWidth, Height = UI_ContentControl_WorkSpace.ActualHeight });
        }
    }
}