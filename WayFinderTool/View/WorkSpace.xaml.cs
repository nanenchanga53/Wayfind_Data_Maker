using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WayFinderTool.Message;
using WayFinderTool.ViewModel;
using WayFindToolCommon.Model;

namespace WayFinderTool.View
{
    /// <summary>
    /// WorkSpace.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WorkSpace : UserControl
    {
        private Point _lastMousePoint = new Point();
        
        public NodeDotViewModel SelectedNode { get; set; }

        public double WorkSpaceActualWidth { get; set; }
        public double WorkSpaceActualHeight { get; set; }

        public WorkSpace()
        {
            InitializeComponent();
            Loaded += delegate
            {
                Messenger.Default.Register<SetWorkSpaceActualSizeMessage>(this, x => SetWorkSpaceActualSize(x.Width, x.Height));
            };

            Unloaded += delegate
            {
                Messenger.Default.Unregister<SetWorkSpaceActualSizeMessage>(this);
            };

        }

        private void SetWorkSpaceActualSize(double width, double height)
        {
            WorkSpaceActualWidth = width;
            WorkSpaceActualHeight = height;
        }

        public void InitGrid()
        {
            //UI_GridLayoutRoot.Width = UI_ImageBaseMap.ActualWidth;
            //UI_GridLayoutRoot.Height = UI_ImageBaseMap.ActualHeight;
            UI_GridElemantsWrapper.Width = UI_ImageBaseMap.ActualWidth;
            UI_GridElemantsWrapper.Height = UI_ImageBaseMap.ActualHeight;
            UI_CanvasMatrixGrid.Width = UI_ImageBaseMap.ActualWidth;
            UI_CanvasMatrixGrid.Height = UI_ImageBaseMap.ActualHeight;
            UI_CanvasNode.Width = UI_ImageBaseMap.ActualWidth;
            UI_CanvasNode.Height = UI_ImageBaseMap.ActualHeight;
        }

        private void UI_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(UI_ImageBaseMap);

            if (e.ClickCount > 1)
            {
                InitGrid();

                Messenger.Default.Send<WorkSpaceDoubleClickMessage>(new WorkSpaceDoubleClickMessage() {Px = position.X, Py = position.Y });
            }
        }

        private void UI_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Get Selected Node
                // Move
                Point position = e.GetPosition(UI_GridElemantsWrapper);
                Messenger.Default.Send<MouseMoveMessage>(new MouseMoveMessage() { Px = position.X, Py = position.Y });
            }
            else if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (UI_GridElemantsWrapper.IsMouseCaptured)
                {
                    UIElement element = sender as UIElement;
                    Point position = e.GetPosition(this);
                    MatrixTransform transform = element.RenderTransform as MatrixTransform;
                    Matrix matrix = transform.Matrix;

                    double targetWidth = UI_ImageBaseMap.ActualWidth;
                    double scaledTargetWidth = targetWidth * matrix.M11;
                    double targetHeight = UI_ImageBaseMap.ActualHeight;
                    double scaledTargetHeight = targetHeight * matrix.M11;

                    //double minOffsetX = ((scaledTargetWidth / 4) * matrix.M11) * -1;
                    //double minOffsetX = (scaledTargetWidth - (550 * matrix.M11)) * -1;
                    //double maxOffsetX = targetWidth / 4;
                    //
                    //double minOffsetY = (scaledTargetHeight - (300 * matrix.M11)) * -1;
                    //double maxOffsetY = targetHeight / 4;

                    //double minOffsetX = (scaledTargetWidth - ((UI_GridLayoutRoot.ActualWidth / matrix.M11) * 2)) * -1;
                    //double maxOffsetX = targetWidth / 4;
                    //
                    //double minOffsetY = (scaledTargetHeight - ((UI_GridLayoutRoot.ActualHeight / matrix.M11) * 2)) * -1;
                    //double maxOffsetY = targetHeight / 4;

                    Messenger.Default.Send(new GetWorkSpaceActualSizeMessage());

                    double scaledViewPotPixelWidth = WorkSpaceActualWidth / matrix.M11;
                    double scaledViewPotPixelHeight = WorkSpaceActualHeight / matrix.M11;

                    Console.WriteLine((scaledTargetWidth - scaledViewPotPixelWidth));

                    double maxOffsetX = scaledViewPotPixelWidth /2;
                    double minOffsetX = (scaledTargetWidth - scaledViewPotPixelWidth) * -1;

                    double maxOffsetY = scaledViewPotPixelHeight /2;
                    double minOffsetY = (scaledTargetHeight - scaledViewPotPixelHeight) * -1;

                    if ((matrix.OffsetX <= maxOffsetX && matrix.OffsetX >= minOffsetX)
                        && (matrix.OffsetY <= maxOffsetY && matrix.OffsetY >= minOffsetY)
                       )
                    {
                        Vector shift = position - _lastMousePoint;
                        //Thickness margin = new Thickness();
                        //margin = UI_GridElemantsWrapper.Margin;
                        //margin.Left += shift.X * matrix.M11;
                        //margin.Top += shift.Y * matrix.M11;
                        //UI_GridElemantsWrapper.Margin = margin;
                        matrix.OffsetX += shift.X;//matrix.M11 > 1 ? shift.X / (matrix.M11/2) : shift.X * matrix.M11;
                        matrix.OffsetY += shift.Y;//matrix.M11 > 1 ? shift.Y / (matrix.M11/2) : shift.Y * matrix.M11;

                        if (matrix.OffsetX >= maxOffsetX)
                        {
                            matrix.OffsetX = maxOffsetX;
                        }
                        if (matrix.OffsetX <= minOffsetX)
                        {
                            matrix.OffsetX = minOffsetX;
                        }
                        if (matrix.OffsetY >= maxOffsetY)
                        {
                            matrix.OffsetY = maxOffsetY;
                        }
                        if (matrix.OffsetY <= minOffsetY)
                        {
                            matrix.OffsetY = minOffsetY;
                        }

                        element.RenderTransform = new MatrixTransform(matrix);
                        _lastMousePoint = position;

                        //Console.WriteLine(minOffsetX + "~" + maxOffsetX + "\t" + matrix.OffsetX + "," + matrix.OffsetY);

                        InitGrid();
                    }
                    else
                    {
                        //if (matrix.OffsetX > 0)
                        //{
                        //    matrix.OffsetX = maxOffsetX;
                        //}
                        //else
                        //{
                        //    matrix.OffsetX = minOffsetX;
                        //}
                        //
                        //if (matrix.OffsetY > 0)
                        //{
                        //    matrix.OffsetY = maxOffsetY;
                        //}
                        //else
                        //{
                        //    matrix.OffsetY = minOffsetY;
                        //}

                        if (matrix.OffsetX >= maxOffsetX)
                        {
                            matrix.OffsetX = maxOffsetX;
                        }
                        if (matrix.OffsetX <= minOffsetX)
                        {
                            matrix.OffsetX = minOffsetX;
                        }
                        if (matrix.OffsetY >= maxOffsetY)
                        {
                            matrix.OffsetY = maxOffsetY;
                        }
                        if (matrix.OffsetY <= minOffsetY)
                        {
                            matrix.OffsetY = minOffsetY;
                        }

                        element.RenderTransform = new MatrixTransform(matrix);
                    }
                }
            }
        }

        private void UI_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            UIElement element = sender as UIElement;
            Point position = e.GetPosition(element);
            MatrixTransform transform = element.RenderTransform as MatrixTransform;
            Matrix matrix = transform.Matrix;
            double scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1);

            double minScale = 1.0;
            double maxScale = 2.5;

            if (e.Delta > 0)
            {
                if (matrix.M11 > maxScale)
                {
                    matrix.M11 = maxScale;
                    matrix.M22 = maxScale;
                    position.X = 0;
                    position.Y = 0;
                    matrix.ScaleAt(scale, scale, position.X, position.Y);
                    return;
                }
            }
            else
            {
                if (matrix.M11 < minScale)
                {
                    matrix.M11 = minScale;
                    matrix.M22 = minScale;
                    position.X = 0;
                    position.Y = 0;
                    double px = (UI_GridLayoutRoot.ActualWidth / 2) - ((UI_ImageBaseMap.ActualWidth * matrix.M11) /2);
                    double py = (UI_GridLayoutRoot.ActualHeight / 2) - ((UI_ImageBaseMap.ActualHeight * matrix.M11) / 2);
                    UI_GridElemantsWrapper.Margin = new Thickness(0, 0, 0, 0);
                    matrix.ScaleAt(scale, scale, position.X, position.Y);
                    matrix.OffsetX = 0;
                    matrix.OffsetY = 0;
                    element.RenderTransform = new MatrixTransform(matrix);
                    return;
                }
            }

            matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);
            element.RenderTransform = new MatrixTransform(matrix);
        }

        private void UI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //_lastMousePoint = e.GetPosition(this);
            //UI_GridElemantsWrapper.CaptureMouse();


        }

        private void UI_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            //(DataContext as WorkSpaceViewModel).SelectedNode = null;
            //UI_GridElemantsWrapper.ReleaseMouseCapture();
            //
            //_lastMousePoint = new Point();
            //if (SelectedNode != null)
            //{
            //    PrevSelectedNode = SelectedNode;
            //    PrevSelectedNode.Id = SelectedNode.Id;
            //    SelectedNode = null;
            //}
        }

        private void UI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                _lastMousePoint = e.GetPosition(this);
                UI_GridElemantsWrapper.CaptureMouse();
            }
        }

        private void UI_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Messenger.Default.Send<MouseLeftPressedMessage>(new MouseLeftPressedMessage() { Pressed = false });
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Released)
            {
                UI_GridElemantsWrapper.ReleaseMouseCapture();
                
                //_lastMousePoint = new Point();
                //if (SelectedNode != null)
                //{
                //    PrevSelectedNode = SelectedNode;
                //    PrevSelectedNode.Id = SelectedNode.Id;
                //    SelectedNode = null;
                //}
            }
        }
    }
}
