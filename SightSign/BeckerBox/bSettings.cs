using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using Tobii_Eris_Library;
using System.Windows.Shapes;
using System.Windows.Media;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // !!! IMPORTANT !!! 
        // Set different "object" for different "lock" block
        private object _lockz = new object();
        private object _lockInMains = new object();
        private object _lockInMainBoardGridClick_helpers = new object();
        private object _lockMouseLeaveBtns = new object();

        private TextBlock _lockedSender = null;
        // For testing
        private StatisticsLogger m_s2;

        private List<ControlsXYandWidthHeight> MainBoxInTheView = new List<ControlsXYandWidthHeight>();
        //private List<ControlsXYandWidthHeight> MinBoxInTheView { get; set; }

        private Ellipse currentDot = new Ellipse();
        private int dotSize = 30;

        private enum BoardType
        {
            MainBoard,
            InnerBoxBoard
        }

        private enum BoxSection
        {
            Up,
            Left,
            Right,
            Down
        }

        //All the becker boxes
        private BoardType mCurrentBoard;
        private List<TextBlock> mMainBoardBoxes;

        //Borders
        private List<Border> mMainBorders;
        
        private MathHelper.Point2D mScreenCoordinates;

        private void setUpTheBBDot()
        {
            currentDot.Stroke = new SolidColorBrush(Colors.Red);
            currentDot.StrokeThickness = 3;
            Canvas.SetZIndex(currentDot, 3);
            currentDot.Height = dotSize;
            currentDot.Width = dotSize;
            currentDot.Fill = new SolidColorBrush(Colors.Red);
            bbMainBoard.Children.Add(currentDot);
        }

        private void addBoardBoxes()
        {
            //Maximumize the window (Becker Board)
            //Application.Current.MainWindow.WindowState = WindowState.Maximized;

            //Hide cursor, since we are controlling the cursor
            //Mouse.OverrideCursor = Cursors.None; //temp

            mMainBoardBoxes = new List<TextBlock>();
            mMainBoardBoxes.Add(ABCD_Box);
            mMainBoardBoxes.Add(EFGH_Box);
            mMainBoardBoxes.Add(IJLK_Box);
            mMainBoardBoxes.Add(OTHER_Box);
            mMainBoardBoxes.Add(MNOP_Box);
            mMainBoardBoxes.Add(QRST_Box);
            mMainBoardBoxes.Add(UVWX_Box);
            mMainBoardBoxes.Add(YZ_Box);

            mMainBorders = new List<Border>();
            mMainBorders.Add(ABCD_Border);
            mMainBorders.Add(EFGH_Border);
            mMainBorders.Add(IJKL_Border);
            mMainBorders.Add(OTHER_Border);
            mMainBorders.Add(MNOP_Border);
            mMainBorders.Add(QRST_Border);
            mMainBorders.Add(UVWX_Border);
            mMainBorders.Add(YZ_Border);

            mScreenCoordinates = new MathHelper.Point2D();

            ShowMainBoard();
        }

        private class PointXY
        {
            public double X = 0.0;
            public double Y = 0.0;

            public PointXY(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
